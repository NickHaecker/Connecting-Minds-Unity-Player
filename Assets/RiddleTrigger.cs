using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleTrigger : MonoBehaviour
{
    public GameObject Riddletext;

    void Start()
    {
        Riddletext.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Riddletext.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        Riddletext.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Riddletext.SetActive(false);
    }
}
