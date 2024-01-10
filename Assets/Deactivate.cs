using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivate : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;

    public void ActiveFalse()
    {
        Debug.Log("Funktion ActiveFalse wurde gedrückt");
        ui.SetActive(false);
    }

    private void OnEnable()
    {

        StartCoroutine(Close());
    }
    IEnumerator Close()
    {
        yield return new WaitForSeconds(2);
        ui.SetActive(false);
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
