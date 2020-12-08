using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class online : MonoBehaviourPunCallbacks
{
   
    public int currRoom;
    public void Awake()
    {        
        Connect();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {   
        base.OnConnectedToMaster();
    }
    public override void OnJoinedRoom()
    {   
        StartGame();
        base.OnJoinedRoom();
        


    }

    public void UpdatePlayerList()
    {
        
     Debug.Log(PhotonNetwork.PlayerList);



    }
    private void Update()
    {
        Debug.Log(PhotonNetwork.PlayerList);

    }
    
    public void Connect()
    {
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void joinlobby()
    {   
     if (PhotonNetwork.IsConnected)
     {
        currRoom = 1;
        PhotonNetwork.JoinRoom("lobby");
     }else
     {
         Debug.Log("You are not connected");
     }
    }

    public void createlobby()
    {
      PhotonNetwork.CreateRoom("lobby");
    }



    public void jointag()
    {
      if (PhotonNetwork.IsConnected)
      {
        currRoom = 2;
        PhotonNetwork.JoinRoom("tag");
      }else
      {
          Debug.Log("You are not connected ");
      }
    }

    public void createtag()
    {
    
      PhotonNetwork.CreateRoom("tag");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {   

        if (currRoom == 1)
       {
           createlobby();
       }else if (currRoom == 2)
       {
           createtag();
       }
        base.OnJoinRandomFailed(returnCode, message);
    }

   
    public void StartGame()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        { 
           
              PhotonNetwork.LoadLevel(2);
           
        }
    }
}
