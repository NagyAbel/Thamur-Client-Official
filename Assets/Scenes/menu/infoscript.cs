using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
public class infoscript : MonoBehaviour
{   
    [SerializeField] private TextMeshProUGUI infotext;
    // Start is called before the first frame update
    void Start()
    {   
        if (PlayerPrefs.GetInt("isLogged") == 0)
       {
         SceneManager.LoadScene(0);
        }else 
        {
         infotext.text = "Logged in as " + PlayerPrefs.GetString("username");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void log()
    {
      StartCoroutine(LogOut());

    }


    IEnumerator LogOut() {
        
        UnityWebRequest www = UnityWebRequest.Get("https://thamur.com/account/logout.php");
        yield return www.SendWebRequest();
        Debug.Log("Logged Out");
        PlayerPrefs.SetInt("isLogged",0);
        PlayerPrefs.SetString("username","");
              SceneManager.LoadScene(0);


    }

}
