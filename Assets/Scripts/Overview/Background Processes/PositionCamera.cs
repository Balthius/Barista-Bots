using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PositionCamera : MonoBehaviour
{
    [SerializeField] private int pixelUnits = 100;
    void Start()
    {
        Camera MainCam = this.GetComponent<Camera>();

        MainCam.orthographicSize = Screen.height / pixelUnits / 2;
        
    }
}