using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ited;
    [SerializeField]private GameObject item;

    public void activate()
    {
        item.SetActive(true);
    }

    public void deactivate() 
    { 
        item.SetActive(false);
    }
}
