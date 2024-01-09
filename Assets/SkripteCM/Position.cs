using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public PositionData posdat;
    [SerializeField] private GameObject position;

    [SerializeField] private Paths gate;
    [SerializeField]
    private List<Position> newPositions;

    [SerializeField] private bool rewarded = false;


    public Paths GetPath()
    {
        return gate;
    }
    public List<Position> GetRewardPositions()
    {
        return newPositions;
    }
    public bool GetRewarded()
    {
        return rewarded;
    }
    public void SetRewared() { rewarded = true; }
}
