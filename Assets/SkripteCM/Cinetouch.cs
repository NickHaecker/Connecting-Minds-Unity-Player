using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinetouch : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook cineCam;
    [SerializeField] TouchField touchField;
    [SerializeField] float SensitivityX = 2f;
    [SerializeField] float SensitivityY = 2f;


    // Update is called once per frame
    void Update()
    {
        cineCam.m_XAxis.Value += touchField.TouchDist.x * 200 * SensitivityX * Time.fixedDeltaTime;
        cineCam.m_YAxis.Value += touchField.TouchDist.y * SensitivityY * Time.fixedDeltaTime;
    }
}
