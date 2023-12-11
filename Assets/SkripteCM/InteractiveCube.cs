using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveCube : MonoBehaviour
{
    public GameObject iteminsert;
    public bool collide = false;
    // Start is called before the first frame update
    void Start()
    {
        iteminsert.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("An object entered.");
        collide = true;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("An object is still inside of the trigger");
        collide = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("An object left.");
        collide = false;
    }

    void Update()
    {
        if (collide == true)
        {
            iteminsert.SetActive(true);
        }
        else
        {
            iteminsert.SetActive(false);
        }
    }

}
