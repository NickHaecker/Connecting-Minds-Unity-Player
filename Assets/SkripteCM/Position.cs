using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Position : MonoBehaviour
{
    public PositionData posdat;
    [SerializeField] private GameObject position;
    [SerializeField] Item item;
    [SerializeField] ItemData target;
    [SerializeField] bool itemnamecorrect = false;
    [SerializeField] public UnityEvent<bool> itemwasplaced; // = new UnityEvent<bool>();

    //[SerializeField] private Paths gate;




    //public Paths GetPath()
    //{
    //    return gate;
    //}

    public void takeItem(Item item)
    {
        this.item = item;
        item.SetPosition(this);
        itemnamecorrect = item.ited.Name == target.Name;
        itemwasplaced?.Invoke(this.item!=null);
    }

    public bool placedCorrect()
    {
        if (this.item == null) 
        {
           return false;
        }

        

        return itemnamecorrect;
    }
    public bool isItemPlaced()
    {
        return this.item != null;
    }

    public void removeItem(Item item)
    {
        if (this.item == null)
        {
            return;
        }
        if (this.item == item)
        {
            this.item = null;
            itemnamecorrect = false;v
            itemwasplaced?.Invoke(this.item != null);
        }
    }


}
