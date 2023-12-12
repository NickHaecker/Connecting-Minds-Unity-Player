using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button createSession;
    public Button options;
    public Button info;
    public AudioSource audio;
    public GameObject optiontext;
    public GameObject infotext;
    private LoadingSceneManager loader;
    private bool openedWindow;
    // Start is called before the first frame update
    void Start()
    {
        optiontext.SetActive(false);
        infotext.SetActive(false);
        createSession.onClick.AddListener(StartSession);
        options.onClick.AddListener(OptionsClicked);
        info.onClick.AddListener(InfoClicked);
        loader = new LoadingSceneManager();
        openedWindow = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartSession()
    {
        audio.Play();

        if(openedWindow == true) 
        {
            optiontext.SetActive (false);
            infotext.SetActive(false);
        }
        loader.LoadLevel(1);
        //SceneManager.LoadScene("Dengeon");
        Debug.Log("Create Session wurde geklickt und wird gestartet");
    }

    private void OptionsClicked()
    {
        audio.Play();

        if (optiontext.activeSelf == true)
        {
            Debug.Log("In der If schleife drin mit Optiontext == true");
            optiontext.SetActive(false);
            openedWindow = false;
        }
        else if(optiontext.activeSelf == false)
        {
            if (infotext == true)
            {
                infotext.SetActive(false);
            }
            Debug.Log("In der If schleife drin mit Optiontext == false");
            optiontext.SetActive(true);
            openedWindow = true;

        }

        Debug.Log("Options wurde geklickt");

    }

    private void InfoClicked()
    {
        audio.Play();
        if (infotext.activeSelf == true)
        {
            Debug.Log("In der If schleife drin mit infotext == true");
            infotext.SetActive(false);
            openedWindow = false;
        }
        else if (infotext.activeSelf == false)
        {
            if (optiontext.activeSelf == true)
            {
                optiontext.SetActive(false);
            }
            Debug.Log("In der If schleife drin mit infotext == false");
            infotext.SetActive(true);
            openedWindow = true;
        }
        Debug.Log("Info wurde geklickt");
    }
}
