using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    public void ActiveFalse()
    {
        ui.SetActive(false);
    }
}
