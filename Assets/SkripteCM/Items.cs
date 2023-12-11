using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private  List<Item> listItems = new List<Item>();
    private List<Position> listPosition = new List<Position>();
    private GameObject item;
    private PathObjects pathobj;
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

    public void getItemFromPlayerTwo(ItemObject item, PositionObject position)
    {
        string itemname = item.Name;

        if (itemname == "KeyCard"&&position.ID=="1") //ID in dem Fall nur ein Exampel 
        {
            /*this.item = GameObject.Find("KeyCard");
            //this.item.transform.position = position;
            //storeItem(item);
            this.item.SetActive(true);*/
            Item i = listItems.Find(item => item.ited.Name == itemname);
            this.item = GameObject.Find(itemname);
            this.item.SetActive(true);
            pathobj.moveObject(position.ID);

        }
    }
}
