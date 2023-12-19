using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;


public class ClientListener : MonoBehaviour
{
    [SerializeField] private BaseNetworkController networkController;
    public Items items;
    private WebSocket webSocket;
    private bool playerTwoConnected = false;
    public GameObject waitingForPlayerPanel;
    private Vector3 placeholder;
    // Start is called before the first frame update
    void Start()
    {
        SendEvent send = new SendEvent("INIT_PLAYER");
        NetworkSingleton.Instance.GetNetworkController().TakeEvent += onTakeEvent;
       
        webSocket = NetworkSingleton.Instance.GetNetworkController().GetSocket();
        //networkController.AfterWebSocketConnected += onAfterWebSocketConnected;
        NetworkSingleton.Instance.GetNetworkController().BeforeWebSocketDisconnected += onBeforeWebSocketDisconnected;
        //networkController.Connect();
        //waitingForPlayerPanel.SetActive(true);
    }

    /*private void onAfterWebSocketConnected(WebSocket webSocket)
    {
        this.webSocket = webSocket;
        SendEvent send = new SendEvent("CONNECT_PLAYER_ONE"); "CREATE_SESSION"
        webSocket.Send(send.ToJson());
        Debug.Log("Player One Connected");
    }*/



    private void onBeforeWebSocketDisconnected(WebSocket webSocket)
    {
        this.webSocket = webSocket;
        //SendEvent send = new SendEvent("DISCONNECT_PLAYER_ONE");
        SendEvent send = new SendEvent("LEAVE_SESSION");
        webSocket.Send(send.ToJson());
        Debug.Log("Player One Disconnected");
    }


    private void onTakeEvent(ReceivedEvent revent)
    {
        if(revent.eventName == "WAIT_FOR_WATCHER")
        {
            Debug.Log("WAIT_FOR_WATCHER Event");
        }
        if (revent.eventName == "WATCHER_EXISTING")
        {
            Debug.Log("WATCHER_EXISTING Event");
        }
        if (revent.eventName == "ON_PLACE_ITEM")
        {
            ItemToPlace item = JsonConvert.DeserializeObject<ItemToPlace>(revent.GetBody()["Items"].ToString());
            
            OnPlaceItem(item); // provisorisch bis ich das Gameobject von Suzan bekomme
        }
        if (revent.eventName == "ON_REMOVE_ITEM")
        {
            ItemToRemove item = JsonConvert.DeserializeObject<ItemToRemove>(revent.GetBody()["Items"].ToString());

            Debug.Log("OnRemoveItem Event");
            OnRemoveItem(item);
        }
        if (revent.eventName == "ON_UNLOCK_PATH")
        {
            Debug.Log("OnUnlockPath Event");
            OnUnlockPath();
        }
        if (revent.eventName == "SEND_MESSAGE")
        {
            Debug.Log("SEND_MESSAGE Event");
            OnSendMessage();
        }
        if (revent.eventName == "MISSING")
        {
            Debug.Log("MISSING Event");
            OnMissing();
        }
        if (revent.eventName == "ON_LEAVE_SESSION")
        {
            Debug.Log("ON_LEAVE_SESSION Event");
            OnLeaveSession();
        }
        if (revent.eventName == "NOT_IN_SESSION")
        {
            Debug.Log("NOT_IN_SESSION Event");
            OnNotInSession();
        }
        if (revent.eventName == "WRONG_PLAYER")
        {
            Debug.Log("WRONG_PLAYER Event");
            OnWrongPlayer();
        }
    }

    private void OnApplicationQuit()
    {
        SendEvent disconnect = new SendEvent("LEAVE_SESSION");
        disconnect.AddData("Type", "PLAYER");
        webSocket.Send(disconnect.ToJson());
        networkController.Disconnect();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWrongPlayer()
    {

    }
    void OnNotInSession()
    {

    }
    void OnLeaveSession()
    {

    }
    
    void OnSendMessage()
    {

    }
    
    void OnMissing()
    {

    }
    void OnPlaceItem(ItemToPlace itemplace)
    {
        Debug.Log("OnPlaceItem Methodenaufruf");
        //Items item = new Items();
        items.getItemFromPlayerTwo(itemplace.Item, itemplace.Position);

    }
    void OnRemoveItem(ItemToRemove itemplace)
    {
        Debug.Log("OnRemoveItem Methodenaufruf");
        //Items item = new Items();
        items.removeItemFromPlayerTwo(itemplace.Item, itemplace.Position);

    }
    void OnUnlockPath()
    {
        Debug.Log("OnUnlockPath Methodenaufruf");
    }
    public struct ItemToPlace
    {
        public ItemObject Item;
        public PositionObject Position;
    }
    public struct ItemToRemove
    {
        public ItemObject Item; 
        public PositionObject Position;
    }
}
