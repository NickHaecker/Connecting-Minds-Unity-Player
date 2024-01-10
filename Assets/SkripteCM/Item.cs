using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData ited;
    [SerializeField]private GameObject item;
    [SerializeField]
    private List<Position> targets;

    [SerializeField]
    private bool correctPlaced = false;


    public void activate()
    {
        item.SetActive(true);
        Debug.Log(item.name+" wurde aktiviert");
    }

    public void deactivate() 
    { 
        item.SetActive(false);
        Debug.Log(item.name + " wurde deaktiviert");
    }
    public void SetPosition(Position position)
    {
        item.transform.position = position.transform.position;
        item.transform.rotation = position.transform.rotation;

        Debug.Log("Setze "+item.name + " auf Position"+ position.name);
    }
    public bool IsTarget(CMPosition position)
    {
        //return position.Name == target.posdat.Name;
        bool iscontaining = false;
        Debug.Log("isTarget Funktion mit iscontaining: "+iscontaining);
        foreach(Position pos in targets)
        {
            if(position.Name == pos.posdat.Name)
            {
                iscontaining = true;
                Debug.Log("isTarget Funktion in foreach mit iscontaining: " + iscontaining + "und Position: "+ pos.name +" und item"+ item.name);
            }
        }


        return iscontaining;
    }
    public void SetPlacedState(bool state)
    {
        correctPlaced = state;
        Debug.Log(item.name + " wurde korrekt Platziert");

    }
    public bool GetPlacedState()
    {
        return correctPlaced;
    }
}
