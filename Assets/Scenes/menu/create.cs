using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Realtime;
public class create : MonoBehaviourPunCallbacks
{   
    private byte maxPlayersPerRoom = 4;
    public TMP_InputField roomname;
    [SerializeField] private TextMeshProUGUI errortext;
    private bool isConnected = false;
    
   public void Awake()
    {        
        PhotonNetwork.AutomaticallySyncScene = true;
        Connect();
    }

    public override void OnConnectedToMaster()
    {   
        isConnected = true;
        Debug.Log("created");
        base.OnConnectedToMaster();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined");
    }
    public override void OnJoinedRoom()
    {   
        Debug.Log("Joind room");
        StartGame();
        base.OnJoinedRoom();
        


    }
    public void back()
    {
                 SceneManager.LoadScene(0);

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {   
        Debug.Log("wtf");
        base.OnJoinRandomFailed(returnCode, message);
    }




    
   
  


    public void Connect()
    {
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void Join()
    {
        
      PhotonNetwork.JoinRoom(roomname.text);
    }
    public void Create()
    {
        if (PlayerPrefs.GetInt("isLogged") == 1 && roomname.text != "" && isConnected == true)
        {
            PhotonNetwork.CreateRoom(roomname.text,new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            if (PhotonNetwork.IsConnected)
            {
               Join();
             } 

        }else
        {
            errortext.text = "Something wrong";
        }

    }
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.LoadLevel(2);
        }
    }


}
