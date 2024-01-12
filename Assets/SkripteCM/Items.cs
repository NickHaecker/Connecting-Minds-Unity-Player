using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private List<Item> listItems = new List<Item>();
    [SerializeField] private List<Position> positions = new List<Position>();

    [SerializeField] private PathObjects pathobj;


    public void PlaceItem(PlacedItem[] placedItems)
    {

        foreach(Item item in listItems)
        {
            if (item.IsInList(placedItems))
            {
                if (!item.IsActive())
                {
                    //Position target;
                    //CMItem cI = placedItems.ToList().Find(pI => pI.Item.Name == item.ited.Name).Item;
                    PlacedItem placedItem = placedItems.ToList().Find(p => p.Item.Name == item.ited.Name);
                    //Item i = listItems.Find(item => item.ited.Name == placedItem.Item.Name);
                    Position pos = positions.Find(p => p.posdat.ID == placedItem.Position.ID);
                    pos.takeItem(item);
                }
            }
            else
            {
                if (item.IsActive())
                {
                    item.deactivate();
                }
            }
        }

        //string itemname = item.Name;
//CMItem item, CMPosition position

        //Item i = listItems.Find(item => item.ited.Name == itemname);
        //Position pos = positions.Find(p => p.posdat.ID == position.ID);

 
        //pos.takeItem(i);

        /*if (pos.placedCorrect())//i.IsTarget(position))
        {
                   //i.SetPosition(pos);
        //i.activate();
            //i.SetPlacedState(true);
            //Paths paths = pos.GetPath();
            //if (paths != null && !paths.GetIsUnlocked())
            //{

            //    paths.Unlock();
            //    PathData path = paths.pathdata;
            //    List<PositionData> positionDatas = pos.GetPath().positions;
            //    PositionObject[] positionObjects = new PositionObject[positionDatas.Count];

            //    for (int y = 0; y < positionDatas.Count; y++)
            //    {
            //        positionObjects[y] = positionDatas[y].GetData<PositionObject>();
            //    }


            //    SendEvent unlockPath = new SendEvent("UNLOCK_PATH");

            //    UnlockedPath unlockedPath = new UnlockedPath { Path = path.GetData<PathObject>(), Positions = positionObjects };
            //    unlockPath.AddData("UnlockedPath", unlockedPath);
            //    NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(unlockPath.ToJson());

            //}
            //if (!pos.GetRewarded())
            //{


            //    if(pos.GetRewardPositions().Count == 0)
            //    {
            //        return;
            //    }

                //pos.SetRewared();


            //foreach (Position rewardPosition in pos.GetRewardPositions())
            //{
            //    SendEvent unlockPosition = new SendEvent("UNLOCK_POSITION");
            //    unlockPosition.AddData("Position", rewardPosition.posdat.GetData<PositionObject>());
            //    NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(unlockPosition.ToJson());
            //}
            //}

        }*/

    }


    /*public void RemoveItem(CMItem item, CMPosition position)
    {
        string itemname = item.Name;


        Item i = listItems.Find(item => item.ited.Name == itemname);
        i.deactivate();

    }*/

    public void deactivateAll()
    {
        foreach (Item item in listItems)
        {
            //item.SetPlacedState(false);
            item.deactivate();
        }
    }
}
