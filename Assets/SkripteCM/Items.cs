using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private  List<Item> listItems = new List<Item>();
    private PathObjects pathobj;
   

    public void getItemFromPlayerTwo(CMItem item, CMPosition position)
    {
        string itemname = item.Name;

        if (itemname == "KeyCard"&&position.ID=="1") //ID in dem Fall nur ein Exampel 
        {

            Item i = listItems.Find(item => item.ited.Name == itemname);
            i.activate();
            pathobj.moveObject(position.ID);

        }
    }

    public void removeItemFromPlayerTwo(CMItem item, CMPosition position)
    {
        string itemname = item.Name;

        if (itemname == "KeyCard" && position.ID == "1") //ID in dem Fall nur ein Exampel 
        {
            Item i = listItems.Find(item => item.ited.Name == itemname);
            i.deactivate();

        }
    }

    public void deactivateAll()
    {
       foreach (Item item in listItems)
        {
            item.deactivate();
        }
    }
}
