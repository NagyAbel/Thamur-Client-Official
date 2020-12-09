using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class chatVisualizer : MonoBehaviourPunCallbacks
{
 public int MaxMessages = 25;
   public  ScrollRect m_ScrollRect;

    public GameObject chatPanel;
    public TextMeshProUGUI textObject;
    public TextMeshProUGUI nameList;

    [SerializeField]
    List<Message> messageList = new List<Message>();
    void Start()
    {
        UpdatePlayerList();   
    }

    void  UpdatePlayerList()
    {
        nameList.text = "";
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {   
            Debug.Log(PhotonNetwork.PlayerList[i]);
            nameList.text  += "\n" +PhotonNetwork.PlayerList[i];
        }
    }

    public override void OnJoinedRoom()
    {   
        UpdatePlayerList();
    }
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.L))
        {   
            
             UpdatePlayerList();
             Debug.Log("Updated Player List");
        }

    }

    public void SendMessageToChat(string text)
    {   
      float backup = m_ScrollRect.verticalNormalizedPosition;

      
           if (messageList.Count >= MaxMessages)
        {
            messageList.Remove(messageList[0]);
            Destroy(messageList[0].textObject);
        }
        Message newMessage = new Message();

        newMessage.text = text;
        TextMeshProUGUI newText = Instantiate(textObject,chatPanel.transform);

        newMessage.textObject = newText;
        newMessage.textObject.text = newMessage.text;
        messageList.Add(newMessage);
        StartCoroutine( ApplyScrollPosition( m_ScrollRect, backup ) );


       
    }

    IEnumerator ApplyScrollPosition( ScrollRect sr, float verticalPos )
    {
    yield return new WaitForEndOfFrame( );
    sr.verticalNormalizedPosition = verticalPos;
    LayoutRebuilder.ForceRebuildLayoutImmediate( (RectTransform)sr.transform );
}  

}


[System.Serializable]
public class Message
{
    public string text;
    public TextMeshProUGUI textObject;
}

