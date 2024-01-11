using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum KindOfReward
{
    POSITION,PATH
}
[Serializable]
public class MultiPlacementWatcher : MonoBehaviour
{
    [SerializeField]
    private KindOfReward reward;

    [SerializeField]
    private List<PositionData> positions = new List<PositionData>();
    [SerializeField] private PathData path;

    [SerializeField]
    private List<Position> watchpositions = new List<Position>();

    [SerializeField] private bool rewareded = false;

    private void Update()
    {
        if (rewareded)
        {
            return;
        }
        int active = 0;

        foreach(Position pos in watchpositions)
        {
            if (pos.placedCorrect())
            {
                active++;
            }
        }


        if(active == watchpositions.Count)
        {
            TakeReward();
        }
    }
    private void TakeReward()
    {
        rewareded = true;


        /*if(reward == KindOfReward.POSITION)
        {
            foreach (PositionData rewardPosition in positions)
            {
                SendEvent unlockPosition = new SendEvent("UNLOCK_POSITION");
                unlockPosition.AddData("Position", rewardPosition.GetData<PositionObject>());
                NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(unlockPosition.ToJson());
            }
        }*/
        if(reward == KindOfReward.PATH)
        {
            PositionObject[] positionObjects = new PositionObject[positions.Count];
            for (int y = 0; y < positions.Count; y++)
            {
                positionObjects[y] = positions[y].GetData<PositionObject>();
            }
            SendEvent unlockPath = new SendEvent("UNLOCK_PATH");

            UnlockedPath unlockedPath = new UnlockedPath { Path = path.GetData<PathObject>(), Positions = positionObjects };
            unlockPath.AddData("UnlockedPath", unlockedPath);
            NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(unlockPath.ToJson());
        }

    }
}
