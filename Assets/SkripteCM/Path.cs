using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paths : MonoBehaviour
{
    public PathData pathdata;
    [SerializeField] private GameObject blocked;
    [SerializeField] private GameObject level;

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
}
