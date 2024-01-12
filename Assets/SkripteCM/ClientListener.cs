using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;

public class ClientListener : MonoBehaviour
{
    [SerializeField] private BaseNetworkController networkController;
    public Items items;
    public PathObjects pathobj;
    public TMP_Text receivedmessage;
    public GameObject messageBox;
    private WebSocket webSocket;
    private bool playerTwoConnected = false;
    public GameObject waitingForPlayerPanel;
    private Vector3 placeholder;
    // Start is called before the first frame update
    void Start()
    {
        messageBox.SetActive(false);
       
        SendEvent send = new SendEvent("INIT_PLAYER");
        networkController = NetworkSingleton.Instance.GetNetworkController();
        NetworkSingleton.Instance.GetNetworkController().TakeEvent += onTakeEvent;
        webSocket = NetworkSingleton.Instance.GetNetworkController().GetSocket();
        webSocket.Send(send.ToJson());

        NetworkSingleton.Instance.GetNetworkController().BeforeWebSocketDisconnected += onBeforeWebSocketDisconnected;
        
    }


    //####################################
    //Events die ich ans WebSocket schicke
    //####################################
    private void onBeforeWebSocketDisconnected(WebSocket webSocket)
    {
        this.webSocket = webSocket;
        
        SendEvent send = new SendEvent("LEAVE_SESSION");
        webSocket.Send(send.ToJson());
        Debug.Log("Player One Disconnected");
    }
    public void OnApplicationQuit()
    {
        SendEvent disconnect = new SendEvent("LEAVE_SESSION");
        disconnect.AddData("Type", "PLAYER");
        webSocket.Send(disconnect.ToJson());
        networkController.Disconnect();
    }

    private void UnlockPath()
    {
        SendEvent unlockPath = new SendEvent("UNLOCK_PATH");
        webSocket.Send(unlockPath.ToJson());

    }
    private void UnlockItem()
    {
        SendEvent unlockItem = new SendEvent("UNLOCK_ITEM");
        webSocket.Send(unlockItem.ToJson());
    }
    private void UnlockPosition()
    {
        SendEvent unlockPosition = new SendEvent("UNLOCK_POSITION");
        webSocket.Send(unlockPosition.ToJson());
    }

    //######################################################
    //Listener Events - Events die ich vom WebSocket bekomme
    //######################################################
    private void onTakeEvent(ReceivedEvent revent)
    {
        if(revent.eventName == "ON_INIT_PLAYER")
        {
            //SessionData sessionData = new SessionData();
            SessionData sessionData = JsonConvert.DeserializeObject<SessionData>(revent.GetBody()["SessionData"].ToString());
            PlacedItem[] items = sessionData.PlacedItems;
            //this.items.deactivateAll();

            //foreach (PlacedItem item in items)
            //{
            //    OnPlaceItem(item);
            //}
            this.items.PlaceItem(items);

            CMPath[] paths = sessionData.UnlockedPaths;
            pathobj.deactivateAll();

            foreach (CMPath path in paths)
            {
                OnUnlockPath(path);
            }

        }
        if(revent.eventName == "ON_UNLOCK_PATH")
        {
            CMPath[] paths = JsonConvert.DeserializeObject<CMPath[]>(revent.GetBody()["Paths"].ToString());
            pathobj.deactivateAll();

            foreach (CMPath path in paths)
            {
                OnUnlockPath(path);
            }

        }
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
            //Bei diesem Event wird ein Item von Spieler 2 an Spieler 1 geschickt
            //Es wird mit revent.getBody()... abgefangen und in item gespeichert
            //die Items sind Objekte von ItemToPlace in dieser Klasse wird spezifiziert
            //was es für Items sind und wo sie platziert werden können
            PlacedItem[] pitems = JsonConvert.DeserializeObject<PlacedItem[]>(revent.GetBody()["Items"].ToString());
            //items.deactivateAll();
            this.items.PlaceItem(pitems);


            //foreach (PlacedItem item in pitems)
            //{
            //    OnPlaceItem(item);
            //}
        }
        if (revent.eventName == "ON_REMOVE_ITEM")
        {
            //Bei diesem Event wird das Item von Spieler 2 wieder aufgesammelt
            //Das Item verschwindet dann bei Spieler 1
            PlacedItem[] pitem = JsonConvert.DeserializeObject<PlacedItem[]>(revent.GetBody()["Items"].ToString());
            //items.deactivateAll();
            this.items.PlaceItem(pitem);

            Debug.Log("OnRemoveItem Event");
            //foreach (PlacedItem item in pitem)
            //{
            //    OnPlaceItem(item);
            //}            
        }
        if (revent.eventName == "SEND_MESSAGE")
        {
            //Bekomme ein Event vom Socket mit dem Feld "Message" und einem String mit der Nachricht 
            // die Nachricht speichere ich in einen String und übergebe es der Funktion
            //diese ruft ein Textfeld im Spiel auf mit der Nachricht (z.B. Error-Message)
            Debug.Log("SEND_MESSAGE Event");
            string s = revent.GetBody()["Message"] as string;
            OnSendMessage(s);
        }
        if (revent.eventName == "MISSING")
        {
            //Bekomme ein Event vom Socket mit dem Feld "Message" und einem String mit der Nachricht 
            // die Nachricht speichere ich in einen String und übergebe es der Funktion
            //Missing sagt aus, dass ein Network-Error aufgetreten ist
            Debug.Log("MISSING Event");
            string s = revent.GetBody()["Message"] as string;
            OnMissing(s);
        }
        if (revent.eventName == "ON_LEAVE_SESSION")
        {
            //Bekomme ein Event vom Socket mit dem Feld "Session" und einem String mit der Nachricht 
            // die Nachricht speichere ich in einen String und übergebe es der Funktion
            //OnLeaveSession sagt aus, dass der Spieler die aus der Session raus gegangen ist
            Debug.Log("ON_LEAVE_SESSION Event");
            string s = revent.GetBody()["Session"] as string;
            OnLeaveSession(s);
        }
        if (revent.eventName == "NOT_IN_SESSION")
        {
            //Not in Session sagt aus, dass man derzeit in keiner Session angemeldet ist
            Debug.Log("NOT_IN_SESSION Event");
            string s = revent.GetBody()["Message"] as string;
            OnNotInSession(s);
        }
        if (revent.eventName == "WRONG_PLAYER")
        {
            //Wrong Player sagt aus, dass der Spieler nicht als "Player" in einer Session angemeldet ist
            Debug.Log("WRONG_PLAYER Event");
            string s = revent.GetBody()["Message"] as string;
            OnWrongPlayer(s);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //######################################################
    //Listener Events - Methoden zu den Events
    //######################################################
    void OnWrongPlayer(string message)
    {
        Debug.Log("OnWrongPlayer abgefangen mit: " + message);
        messageBox.SetActive(true);
        receivedmessage.text = message;
    }
    void OnNotInSession(string message)
    {
        Debug.Log("OnNotInSession abgefangen mit: " + message);
        messageBox.SetActive(true);
        receivedmessage.text = message;
    }
    void OnLeaveSession(string message)
    {
        Debug.Log("OnLeaveSession abgefangen mit: " + message);
        messageBox.SetActive(true);
        receivedmessage.text = message;
    }
    void OnSendMessage(string message)
    {
        Debug.Log("OnSendMessage abgefangen mit: "+message);
        messageBox.SetActive(true);
        receivedmessage.text = message;
    }
    void OnMissing(string message)
    {
        Debug.Log("OnMissing abgefangen mit: " + message);
        messageBox.SetActive(true);
        receivedmessage.text = message;
    }
    void OnPlaceItem(PlacedItem itemplace)
    {
        Debug.Log("OnPlaceItem Methodenaufruf");
        //Items item = new Items();
        //items.PlaceItem(itemplace.Item, itemplace.Position);

    }
    void OnUnlockPath(CMPath path)
    {
        Debug.Log("OnUnlockPath Methodenaufruf");
        pathobj.UnlockPath(path);
    }
   
}
public struct UnlockedPath
{
    public PathObject Path;
    public PositionObject[] Positions;
}