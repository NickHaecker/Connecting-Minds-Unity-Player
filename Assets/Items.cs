using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private List<GameObject> listItems = new List<GameObject>();
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

    private void storeItem(GameObject item)
    {
        listItems.Add(item);
    }

    public void getItemFromPlayerTwo(string itemname, Vector3 position)
    {
        if (itemname == "KeyCard")
        {
            item = GameObject.Find("KeyCard");
            item.transform.position = position;
            storeItem(item);
            item.SetActive(true);
        }
    }
}
