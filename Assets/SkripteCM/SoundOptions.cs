using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using static UnityEngine.Rendering.DebugUI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] Slider soundslider;
    [SerializeField] AudioMixer masterMixer;


    // Start is called before the first frame update
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));

    }

    public void SetVolume(float value)
    {
        if(value < 1)
        {
            value = .001f;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20f);
        Debug.Log("SetVolume --> SavedMasterVolume: "+value);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(soundslider.value);
        
    }

    public void RefreshSlider(float value)
    {
        soundslider.value = value;
        Debug.Log("Refreshslider --> SavedMasterVolume: " + value);
    }

}
