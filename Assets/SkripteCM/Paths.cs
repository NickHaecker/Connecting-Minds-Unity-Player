using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public PathData pathdata;
    public List<PositionData> positions = new List<PositionData>();
    [SerializeField] private GameObject blocked;
    [SerializeField] private GameObject level;

    [SerializeField]
    private bool isUnlocked = false;

    public void activate()
    {
        blocked.SetActive(false);
        level.SetActive(true);
    }

    public void deactivate()
    {
        blocked.SetActive(true);
        level.SetActive(false);
    }
    public void Unlock()
    {
        isUnlocked = true;
    }
    public bool GetIsUnlocked()
    {
        return isUnlocked;
    }
}
