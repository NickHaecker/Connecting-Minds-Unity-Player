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
    // Start is called before the first frame update
    void Start()
    {
        createSession.onClick.AddListener(StartSession);
        options.onClick.AddListener(OptionsClicked);
        info.onClick.AddListener(InfoClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartSession()
    {
        audio.Play();
        SceneManager.LoadScene("Dengeon");
        Debug.Log("Create Session wurde geklickt und wird gestartet");
    }

    private void OptionsClicked()
    {
        audio.Play();
        Debug.Log("Options wurde geklickt");
    }

    private void InfoClicked()
    {
        audio.Play();
        Debug.Log("Info wurde geklickt");
    }
}
