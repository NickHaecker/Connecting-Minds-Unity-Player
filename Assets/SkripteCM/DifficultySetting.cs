using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DifficultySetting : MonoBehaviour
{
    //public TextMeshPro difisetting;
    public TMP_Text difisetting;
    [SerializeField] Slider difficultyslider;
    private float _value = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //SetDifficulty(0);
        _value = 0f;
    }

    private void Update()
    {
        SetDifficulty(_value);
    }

    public void SetDifficulty(float value)
    {
        if(difficultyslider.value == 0)
        {
            _value= difficultyslider.value;
            difisetting.text = "Leicht"; 
        }
        else if (difficultyslider.value == 1)
        {
            _value = difficultyslider.value;
            difisetting.text = "Mittel";
        }
        if (difficultyslider.value == 2)
        {
            _value = difficultyslider.value;
            difisetting.text = "Schwer";
        }
    }
}
