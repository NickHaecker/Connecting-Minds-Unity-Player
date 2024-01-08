using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public PathData pathdata;
    [SerializeField] private GameObject path;

    public void activate()
    {
        path.SetActive(true);
    }

    public void deactivate()
    {
        path.SetActive(false);
    }
}
