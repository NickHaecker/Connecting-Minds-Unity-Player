using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using WebSocketSharp;
using TMPro;
using Newtonsoft.Json;
using static ClientListener;

public class MainMenu : MonoBehaviour
{
    ReceivedEvent receivedEvent;
    public TMP_Text sessionIDText;
    public TMP_Text meldung;
    public TMP_Text sessionIDInput;
    public Button sessionOK;
    public Button createSession;
    public Button options;
    public Button info;
    public Button joinsession;
    public Button createnewsession;
    public AudioSource audiosauce;
    public GameObject optiontext;
    public GameObject infotext;
    public GameObject sessionWindow;
    public GameObject joinSessionWindow;
    [SerializeField] private LoadingSceneManager loader;
    private bool openedWindow;
    private bool joinsessionpressed;
    private bool createsessionpressed;
    [SerializeField] private NetworkController networkController;
    // Start is called before the first frame update
    void Start()
    {
        joinsessionpressed = false;
        createsessionpressed = false;
        receivedEvent = new ReceivedEvent();
        optiontext.SetActive(false);
        infotext.SetActive(false);
        sessionWindow.SetActive(false);
        joinSessionWindow.SetActive(false);
        //loader = new LoadingSceneManager();
        openedWindow = false;
        NetworkSingleton.Instance.GetNetworkController().TakeEvent += onTakeEvent;
        Cursor.visible = false;
    }
    private void onTakeEvent(ReceivedEvent revent)
    {
        if (revent.eventName == "ON_CREATE_SESSION")
        {
            Debug.Log("ON_CREATE_SESSION Event");
            sessionIDText.text = revent.GetBody()["Session"].ToString();
        }
        if (revent.eventName == "ON_JOIN_SESSION")
        {
            Debug.Log("ON_JOIN_SESSION Event");
            meldung.text = revent.GetBody()["Session"].ToString()+" Watcher ist der Session gejoined";
        }
        if (revent.eventName == "SESSION_NOT_FOUND")
        {
            Debug.Log("SESSION_NOT_FOUND Event");
            meldung.text = revent.GetBody()["Message"].ToString();
        }
        if (revent.eventName == "SESSION_IS_OCCUPIED")
        {
            Debug.Log("SESSION_IS_OCCUPIED Event");
            meldung.text = revent.GetBody()["Message"].ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void JoinSession(string sessionIDEingabe)
    {
        WebSocket websocket = networkController.GetSocket();
        SendEvent send = new SendEvent("JOIN_SESSION");
        send.AddData("Type", "PLAYER");
        websocket.Send(send.ToJson());
        joinSessionWindow.SetActive(true);
        Debug.Log("Player joined Session");
    }

    public void JoinSessionPressed() 
    {
        joinsessionpressed = true;
    }
    public void CreateSessionPressed() 
    {
        createsessionpressed = true;
    }

    public void StartSession()
    {
        audiosauce.Play();

        if (openedWindow == true)
        {
            optiontext.SetActive(false);
            infotext.SetActive(false);
        }
        //if (joinsession == true)
        //{
        //    string idses = sessionIDInput.text;
        //    JoinSession(idses);
        //}
        //else if (createsessionpressed == true)
        //{
            WebSocket websocket = networkController.GetSocket();
            SendEvent send = new SendEvent("CREATE_SESSION");
            send.AddData("Type", "PLAYER");
            websocket.Send(send.ToJson());
            sessionWindow.SetActive(true);
            Debug.Log("Create Session wurde geklickt und wird gestartet");
        //}

    }

    public void LoadScene()
    {
        //loader.LoadLevel("Dengeon");
        SceneManagerController.LoadSceneSync("Dengeon",false);
    }

    public void OptionsClicked()
    {
        audiosauce.Play();

        if (optiontext.activeSelf == true)
        {
            Debug.Log("In der If schleife drin mit Optiontext == true");
            optiontext.SetActive(false);
            openedWindow = false;
        }
        else if (optiontext.activeSelf == false)
        {
            if (infotext == true)
            {
                infotext.SetActive(false);
            }
            Debug.Log("In der If schleife drin mit Optiontext == false");
            optiontext.SetActive(true);
            openedWindow = true;

        }

        Debug.Log("Options wurde geklickt");

    }

    public void InfoClicked()
    {
        audiosauce.Play();
        if (infotext.activeSelf == true)
        {
            Debug.Log("In der If schleife drin mit infotext == true");
            infotext.SetActive(false);
            openedWindow = false;
        }
        else if (infotext.activeSelf == false)
        {
            if (optiontext.activeSelf == true)
            {
                optiontext.SetActive(false);
            }
            Debug.Log("In der If schleife drin mit infotext == false");
            infotext.SetActive(true);
            openedWindow = true;
        }
        Debug.Log("Info wurde geklickt");
    }

}
