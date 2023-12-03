using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject obj;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void moveObject(string ID)
    {
        if (ID == "1")
        {
            obj = GameObject.Find("WallOne");
        }
    }
}
