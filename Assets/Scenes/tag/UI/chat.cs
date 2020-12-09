using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;

public class chat : MonoBehaviourPunCallbacks,IChatClientListener
{
       
    public ChatClient chatClient;
    protected internal ChatAppSettings chatAppSettings;
    public TMP_InputField nameText;
    private chatVisualizer ChatVisualizer;
    bool active = false;

    private void Start()
    {   
            conn();
    }
    // On connected to chat server
    public void conn()
	{       
            chatClient = new ChatClient( this );
            chatClient.Connect("583632cc-6cca-48ae-ac2b-0beef0d0e377","0.0.1",new AuthenticationValues(PlayerPrefs.GetString("username")));
	}
    public void Update()
    {
        chatClient.Service();
      
          if (Input.GetKeyDown(KeyCode.Return))
        {   

            send(nameText.text);
            
            nameText.text = "";
           Debug.Log('1');
        }

        

    }
    
  

    //On Connected to a Channel
    public void OnConnected()
	{
        chatClient.Subscribe( new string[] { "channelA" } );

	}
    //Sending Messages
    private void send(string text)
    {
        chatClient.PublishMessage( "channelA", text );
    }
    
    
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
       string msgs = "";
       string sender = "";
       for ( int i = 0; i < senders.Length; i++ )
       {    
            sender = string.Format("{1}", msgs, senders[i], messages[i]);
            msgs = string.Format("{1}:{2} ", msgs, senders[i], messages[i]);
            
       }
      // Debug.Log( "OnGetMessages: {0} ({1}) > {2}"  +  channelName + senders.Length +  msgs );
       //textObject.text += "\n"+msgs;
       
       
       if (msgs != sender + ": " )
        {
         Debug.Log("Text Recieved");
          Debug.Log(msgs);
          Debug.Log(sender + ": "); 
         ChatVisualizer = FindObjectOfType<chatVisualizer>();
         ChatVisualizer.SendMessageToChat(msgs);

       }else 
        {   
            Debug.Log("Text didnt recieved");
            Debug.Log(msgs.Length);
            Debug.Log(msgs);
            Debug.Log(sender + ": "); 
            //Debug.Log(msgs);
            Debug.Log(sender.Length);
        }
       
       

	}
       
       
       
       
       
       
       
       
       
     // Not used chat callbacks
    public void OnDisconnected(){}
    public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message){}
    public void OnChatStateChange(ChatState state){}
    public void OnPrivateMessage(string sender, object message, string channelName){}
    public void OnSubscribed(string[] channels, bool[] results){}
    public void OnUnsubscribed(string[] channels){}
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message){}
    public void OnUserSubscribed(string channel, string user){}
    public void OnUserUnsubscribed(string channel, string user){}

}
