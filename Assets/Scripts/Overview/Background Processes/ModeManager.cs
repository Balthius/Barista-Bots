using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeManager : MonoBehaviour
{

    public GameObject modeFilter;
    void Update()
    {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            modeFilter.GetComponent<Image>().enabled = true;
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            modeFilter.GetComponent<Image>().enabled = false;
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            modeFilter.GetComponent<Image>().enabled = true;
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            modeFilter.GetComponent<Image>().enabled = false;
        }
    }
}