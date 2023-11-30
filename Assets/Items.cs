using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private List<Item> listItems = new List<Item>();
    private GameObject item;
    //private Vector3 positionItem;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void storeItem(GameObject item)
    {
        listItems.Add(item);
    }*/

    public void getItemFromPlayerTwo(ItemData item)
    {
        string itemname = item.Name;

        if (itemname == "KeyCard")
        {
            /*this.item = GameObject.Find("KeyCard");
            //this.item.transform.position = position;
            //storeItem(item);
            this.item.SetActive(true);*/
            Item i = listItems.Find(item => item.ited.Name == itemname);

        }
    }
}
