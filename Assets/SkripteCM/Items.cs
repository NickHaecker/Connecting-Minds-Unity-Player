using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private List<Item> listItems = new List<Item>();
    [SerializeField] private List<Position> positions = new List<Position>();

    [SerializeField] private PathObjects pathobj;


    public void PlaceItem(CMItem item, CMPosition position)
    {
        string itemname = item.Name;

        //if (itemname == "KeyCard"&&position.ID=="1") //ID in dem Fall nur ein Exampel 
        //{

        Item i = listItems.Find(item => item.ited.Name == itemname);
        Position pos = positions.Find(p => p.posdat.Name == position.Name);
        i.SetPosition(pos);
        i.activate();

        if (i.IsTarget(position))
        {
            //pathobj.moveObject(position);
            PathData path = pos.GetPath().pathdata;
            List<PositionData> positionDatas = pos.GetPath().positions;
            PositionObject[] positionObjects = new PositionObject[positionDatas.Count];

            for(int y = 0; y < positionDatas.Count; y++)
            {
                positionObjects[y] = positionDatas[y].GetData<PositionObject>();
            }

            //PositionData

            SendEvent unlockPath = new SendEvent("UNLOCK_PATH");

            UnlockedPath unlockedPath = new UnlockedPath { Path = path.GetData<PathObject>(), Positions = positionObjects };
            unlockPath.AddData("UnlockedPath", unlockedPath);
            NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(unlockPath.ToJson());

        }
        //

        //}
    }


    public void RemoveItem(CMItem item, CMPosition position)
    {
        string itemname = item.Name;

        //if (itemname == "KeyCard" && position.ID == "1") //ID in dem Fall nur ein Exampel 
        //{
        Item i = listItems.Find(item => item.ited.Name == itemname);
        i.deactivate();

        //}
    }

    public void deactivateAll()
    {
        foreach (Item item in listItems)
        {
            item.deactivate();
        }
    }
}
