using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkSingleton : MonoBehaviour
{
    private static NetworkSingleton _instance = null;
    public static NetworkSingleton Instance { get { return _instance; } }
    [SerializeField] private NetworkController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        _controller.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public NetworkController GetNetworkController()
    {
        return _controller;
    }
}
