using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    public PositionData posdat;
    [SerializeField] private GameObject position;

    [SerializeField] private Paths gate;




    public Paths GetPath()
    {
        return gate;
    }


}
