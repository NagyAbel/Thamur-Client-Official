using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
public class joinscript : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public override void OnRoomListUpdate(List<RoomInfo> roomList)
   {
        foreach (RoomInfo room in roomList)
        {
            Debug.Log(room);
        }

        
   }


    public void home()
    {
        SceneManager.LoadScene(1);
    }
  
}
