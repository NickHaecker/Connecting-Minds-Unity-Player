using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionID : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
