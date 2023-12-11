using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Item", menuName = "ConnectingMinds/Item")]
[Serializable]
public class ItemData : Indexable
{
    [HideInInspector]
    public string _id;
    public string Name;
    public string Description;

    public override void AddEventData(SendEvent sendEvent)
    {
        sendEvent.AddData("DataToIndex", new ItemObject(this));
    }

    public override T GetData<T>()
    {
        return new ItemObject(this) as T;
    }
}

public class ItemObject : ScriptableObjectData
{
    public string Name;
    public string Description;
    public string _id;
    public ItemObject(ItemData item)
    {
        Name = item.Name;
        Description = item.Description;
    }

}
