using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSingleton : MonoBehaviour
{
    private static NetworkSingleton _instance = null;
    public static NetworkSingleton Instance { get { return _instance; } }
    [SerializeField] private NetworkController _controller;
    // Start is called before the first frame update

    public string sessionID = "";
    void Start()
    {
        _instance = this;
        _controller.Connect();

        _controller.TakeEvent += onTakeEvent;
    }
    private void onTakeEvent(ReceivedEvent revent)
    {
        if (revent.eventName == "ON_CREATE_SESSION")
        {
            Debug.Log("ON_CREATE_SESSION Event");
            sessionID = revent.GetBody()["Session"].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        _controller.TakeEvent -= onTakeEvent;
    }

    public NetworkController GetNetworkController()
    {
        return _controller;
    }
}
