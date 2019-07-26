using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBotUIManager : MonoBehaviour {
    
    public GameObject modeFilter;
    
        void Update()
        {
            if (Input.deviceOrientation == DeviceOrientation.Portrait)
            {
                modeFilter.SetActive(false);
            }
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                modeFilter.SetActive(true);
            }
            if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                modeFilter.SetActive(false);
            }
            if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                modeFilter.SetActive(true);
            }
    }
}