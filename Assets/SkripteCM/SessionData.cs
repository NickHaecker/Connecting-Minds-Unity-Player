using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionData
{
    public bool ContainsWatcher;
    public PlacedItem[] PlacedItems;
    public CMPath[] UnlockedPaths;
    public CMPosition[] UnlockedPositions;
}
public struct CMPath
{
    public string _id;
    public string Name;
    public string Description;
}
public struct PlacedItem
{
    public CMItem Item;
    public CMPosition Position;
}
public struct CMItem
{
    public string _id;
    public string Name;
    public string ID;
    public string Description;
}
public struct CMPosition
{
    public string _id;
    public string Name;
    public string ID;
}