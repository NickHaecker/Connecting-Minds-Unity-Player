using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Position", menuName = "ConnectingMinds/Position")]
[Serializable]
public class PositionData : Indexable
{
    [HideInInspector]
    public string _id;
    public string Name;
    public string ID;

    public override void AddEventData(SendEvent sendEvent)
    {
        sendEvent.AddData("DataToIndex", new PositionObject(this));
    }

    public override T GetData<T>()
    {
        return new PositionObject(this) as T;
    }

}
public class PositionObject : ScriptableObjectData
{
    public string Name;
    public string ID;
    public string _id;
    public PositionObject(PositionData position)
    {
        Name = position.Name;
        ID = position.ID;
    }

}
