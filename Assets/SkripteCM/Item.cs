using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ited;
    [SerializeField]private GameObject item;
    [SerializeField]
    private Position target;


    public void activate()
    {
        item.SetActive(true);
    }

    public void deactivate() 
    { 
        item.SetActive(false);
    }
    public void SetPosition(Position position)
    {
        item.transform.position = position.transform.position;
        item.transform.rotation = position.transform.rotation;
    }
    public bool IsTarget(CMPosition position)
    {
        return position.Name == target.posdat.Name;
    }
}
