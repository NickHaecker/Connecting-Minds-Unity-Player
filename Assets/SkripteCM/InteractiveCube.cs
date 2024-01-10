using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveCube : MonoBehaviour
{
    public GameObject iteminsert;
    public GameObject portalCollider;
    public Button portalGo;
    public bool collide = false;
    ClientListener listener;
    //private LoadingSceneManager loader;
    // Start is called before the first frame update
    void Start()
    {
        iteminsert.SetActive(false);
        portalCollider.SetActive(false);
        //loader = new LoadingSceneManager();
        Button btn = portalGo.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("An object entered.");
        collide = true;
    }
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("An object is still inside of the trigger");
        collide = true;
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("An object left.");
        collide = false;
    }

    private void onClick()
    {
        listener.OnApplicationQuit();
        //loader.LoadLevel("SampleScene");
    }

    void Update()
    {
        if (collide == true)
        {
            iteminsert.SetActive(true);
            portalCollider.SetActive(true);
        }
        else
        {
            iteminsert.SetActive(false);
            portalCollider.SetActive(false);
        }
    }

}
