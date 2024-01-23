using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SessionID : MonoBehaviour
{
    [SerializeField]
    private TMP_Text id;
    void Start()
    {
        id.text = "SessionID: " + NetworkSingleton.Instance.sessionID;
    }
}
