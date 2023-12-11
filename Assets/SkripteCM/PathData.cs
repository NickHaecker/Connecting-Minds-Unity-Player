using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "ConnectingMinds/Path")]
[Serializable]
public class PathData : Indexable
{
    [HideInInspector]
    public string _id;
    public string Name;
    public string Description;

    public override void AddEventData(SendEvent sendEvent)
    {
        sendEvent.AddData("DataToIndex", new PathObject(this));
    }

    public override T GetData<T>()
    {
        return new PathObject(this) as T;
    }
}

public class PathObject : ScriptableObjectData
{
    public string Name;
    public string Description;
    public string _id;
    public PathObject(PathData path)
    {
        Name = path.Name;
        Description = path.Description;
    }

}

