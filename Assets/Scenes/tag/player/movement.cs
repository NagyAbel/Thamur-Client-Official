using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class movement : MonoBehaviourPunCallbacks
{
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject normalCam;
    public GameObject cameraParent;
    public Transform cam;
    public CharacterController controller;
    public float speed = 6f;
    bool isGrounded;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    Vector3 velocity;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

            if (photonView.IsMine)
            {   
                normalCam.SetActive(true);
                cameraParent.SetActive(true);
            }

    }
    void Update()
    {    

        if (!photonView.IsMine) return;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float horizontal  = Input.GetAxisRaw("Horizontal");
        float vertical =  Input.GetAxisRaw("Vertical");
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 direction = new Vector3(horizontal,0f,vertical).normalized;
        
        if (direction.magnitude >=  0.1f)
        {   
            float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity,turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) *  Vector3.forward;
            controller.Move(moveDir.normalized * speed*Time.deltaTime);
        }  
}

}
