using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PositionActivator : MonoBehaviour
{
    [SerializeField] List<Position> watchedpositions = new List<Position>();
    [SerializeField]
    private List<PositionData> positions = new List<PositionData>();
    bool wasactivated = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void TakeReward()
    {
        wasactivated = true;

        foreach (PositionData rewardPosition in positions)
        {
            SendEvent unlockPosition = new SendEvent("UNLOCK_POSITION");
            unlockPosition.AddData("Position", rewardPosition.GetData<PositionObject>());
            NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(unlockPosition.ToJson());
        }
    }
    private void RemoveReward()
    {
        wasactivated = false;

        foreach (PositionData rewardPosition in positions)
        {
            SendEvent removePosition = new SendEvent("REMOVE_POSITION");
            removePosition.AddData("Position", rewardPosition.GetData<PositionObject>());
            NetworkSingleton.Instance.GetNetworkController().GetWebSocket().Send(removePosition.ToJson());
        }
    }
    public void onPlaceItem(bool state)
    {

        int active = 0;

        foreach (Position pos in watchedpositions)
        {
            if (pos.placedCorrect())
            {
                active++;
            }
        }
        if (wasactivated)
        {
            if(active!=watchedpositions.Count)
            {
                RemoveReward();
            }
        }
        else
        {

            if (active == watchedpositions.Count)
            {
                TakeReward();
            }
        }
    }


}
