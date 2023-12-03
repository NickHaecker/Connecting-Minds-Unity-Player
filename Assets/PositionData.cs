using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[CreateAssetMenu(fileName = "Position", menuName = "ConnectingMinds/Item")]
[Serializable]
public class PositionData : Indexable
{


    public string Name;
    public string ID;

    public class PositionObject : ScriptableObjectData
    {
        public string Name;
        public string ID;
        public PositionObject(PositionData position)
        {
            Name = position.Name;
            ID = position.ID;
        }

    }

    public override void AddEventData(SendEvent sendEvent)
    {
        sendEvent.AddData("DataToIndex", new PositionObject(this));
    }

    public override T GetData<T>()
    {
        return new PositionObject(this) as T;
    }

}
