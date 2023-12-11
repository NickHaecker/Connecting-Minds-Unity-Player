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
        networkController.TakeEvent += onTakeEvent;
        networkController.AfterWebSocketConnected += onAfterWebSocketConnected;
        networkController.BeforeWebSocketDisconnected += onBeforeWebSocketDisconnected;
        networkController.Connect();
        //waitingForPlayerPanel.SetActive(true);
    }

    private void onAfterWebSocketConnected(WebSocket webSocket)
    {
        this.webSocket = webSocket;
        SendEvent send = new SendEvent("CONNECT_PLAYER_ONE");
        webSocket.Send(send.ToJson());
        Debug.Log("Player One Connected");
    }

    private void onBeforeWebSocketDisconnected(WebSocket webSocket)
    {
        this.webSocket = webSocket;
        SendEvent send = new SendEvent("DISCONNECT_PLAYER_ONE");
        webSocket.Send(send.ToJson());
        Debug.Log("Player One Disconnected");
    }

    private void onTakeEvent(ReceivedEvent revent)
    {
        if (revent.eventName == "ON_PLACE_ITEM")
        {
            ItemToPlace item = JsonConvert.DeserializeObject<ItemToPlace>(revent.GetBody()["ItemToPlace"].ToString());
            //PositionToSearch pos = JsonConvert.DeserializeObject<PositionToSearch>(revent.GetBody()["PositionToSearch"].ToString());
            OnPlaceItem(item); // provisorisch bis ich das Gameobject von Suzan bekomme
        }
        if (revent.eventName == "ON_CONNECT_PLAYER_TWO")
        {
            Debug.Log("OnConnectPlayerTwo Event");
            OnPlayerTwoConnect();
        }
        if (revent.eventName == "ON_DISCONNECT_PLAYER_TWO")
        {
            Debug.Log("OnDisconnectPlayerTwo Event");
            OnPlayerTwoDisconnect();
        }
        if(revent.eventName == "WAIT_FOR_PLAYER_TWO")
        {
            Debug.Log("WaitForPlayerTwo Event");
            WaitForPlayerTwo();
        }
        if (revent.eventName == "ON_REMOVE_ITEM")
        {
            Debug.Log("OnRemoveItem Event");
            OnRemoveItem();
        }
        if (revent.eventName == "ON_UNLOCK_PATH")
        {
            Debug.Log("OnUnlockPath Event");
            OnUnlockPath();
        }
        if (revent.eventName == "ON_LOAD_POSITION")
        {
            Debug.Log("OnLoadPosition Event");
            OnLoadPosition();
        }
        if (revent.eventName == "ON_UPDATE_ITEM")
        {
            Debug.Log("OnUpdateItem Event");
            OnUpdateItem();
        }
    }

    private void OnApplicationQuit()
    {
        SendEvent disconnect = new SendEvent("DISCONNECT_PLAYER_ONE");
        webSocket.Send(disconnect.ToJson());
        networkController.Disconnect();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlaceItem(ItemToPlace itemplace)
    {
        Debug.Log("OnPlaceItem Methodenaufruf");
        //Items item = new Items();
        items.getItemFromPlayerTwo(itemplace.Item, itemplace.Position);

    }
    void OnPlayerTwoConnect()
    {
        Debug.Log("OnConnectPlayerTwo Methodenaufruf");
        playerTwoConnected = true;
        waitingForPlayerPanel.SetActive(false);
    }

    void WaitForPlayerTwo()
    {
        Debug.Log("WaitForPlayerTwo Methodenaufruf");
        if(playerTwoConnected == false)
        {
            //Time.timeScale = 0;
            waitingForPlayerPanel.SetActive(true);
        }
    }

    void OnPlayerTwoDisconnect()
    {
        Debug.Log("OnDisconnectPlayerTwo Methodenaufruf");
        playerTwoConnected = false;
    }

    void OnRemoveItem()
    {
        Debug.Log("OnRemoveItem Methodenaufruf");
    }

    void OnUnlockPath()
    {
        Debug.Log("OnUnlockPath Methodenaufruf");
    }

    void OnLoadPosition()
    {
        Debug.Log("OnLoadPosition Methodenaufruf");
    }

    void OnUpdateItem() 
    {
        Debug.Log("OnUpdateItem Methodenaufruf");
    }

    public struct ItemToPlace
    {
        public ItemObject Item;
        public PositionObject Position;

        //public string position;
    }

    /*public struct PositionToSearch
    {
        public PositionData Position;
    }*/
}
