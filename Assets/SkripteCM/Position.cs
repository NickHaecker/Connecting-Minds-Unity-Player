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
        itemwasplaced?.Invoke(this.item!=null);
    }

    public bool placedCorrect()
    {
        if (this.item == null) 
        {
           return false;
        }

        itemnamecorrect = item.ited.Name == target.Name;

        return itemnamecorrect;
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
            itemwasplaced?.Invoke(this.item != null);
        }
    }


}
