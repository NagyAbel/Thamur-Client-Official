using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class login : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    [SerializeField] private TextMeshProUGUI infotext;
 
    public bool isLogged;
    public bool passedTest;



    void Start()
    {
      if (PlayerPrefs.GetInt("isLogged") == 1)
      {
      SceneManager.LoadScene(1);

      }
    }
    public void check()
    {   
         if (username.text == ""  || password.text == "")
        {
            infotext.text  = "Username or password can't be empty";
        }else
        {   
            infotext.text = "";
            StartCoroutine(LogIn());
        }
  
    }

    IEnumerator LogIn() {
        WWWForm form = new WWWForm();
        
          form.AddField("username", username.text);
          form.AddField("pw1",password.text);
          UnityWebRequest www = UnityWebRequest.Post("https://thamur.com/account/login/submit.php", form);
           yield return www.SendWebRequest();

            Debug.Log(www.downloadHandler.text);


            if (www.downloadHandler.text != "succ")
            {
                infotext.text = "Wrong username or password";
            }else {


                PlayerPrefs.SetInt("isLogged",1);
                PlayerPrefs.SetString("username",username.text);
                SceneManager.LoadScene(1);
            }
        
 
 
        

    }

    
      
       IEnumerator LogOut() {
        
        UnityWebRequest www = UnityWebRequest.Get("https://thamur.com/account/logout.php");
        yield return www.SendWebRequest();
        Debug.Log("Logged Out");
        PlayerPrefs.SetInt("isLogged",0);
        PlayerPrefs.SetString("username","");


    }

 
}
