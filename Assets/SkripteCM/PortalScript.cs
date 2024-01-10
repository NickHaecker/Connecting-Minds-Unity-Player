using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalScript : MonoBehaviour
{
    public GameObject Portaltext;
    public Button portalGo;
    public bool collide;
    ClientListener listener;
    private LoadingSceneManager loader;
    // Start is called before the first frame update
    void Start()
    {
        collide = false;
        Portaltext.SetActive(false);
        Button btn = portalGo.GetComponent<Button>();
        btn.onClick.AddListener(onClick);
        loader = new LoadingSceneManager();
    }

    // Update is called once per frame
    void Update()
    {
        if (collide == true)
        {
            Portaltext.SetActive(true);
        }
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
        loader.LoadLevel("SampleScene");
    }
}
