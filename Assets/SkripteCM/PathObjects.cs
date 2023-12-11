using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathObjects : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject obj;
    private float movementspeed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveObject(string ID)
    {
        float horizontalinput = Input.GetAxis("Horizontal");

        if (ID == "1")
        {
            obj = GameObject.Find("WallOne");
            obj.transform.position = obj.transform.position + new Vector3(horizontalinput * movementspeed *Time.deltaTime, 0, 0);
        }
    }
}
