using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomerServiceCombat
{
    public class ScreenOrientationManager : MonoBehaviour
    {
        public GameObject landscapeMode;
        public GameObject portraitMode;

        ScreenOrientation prevOrientation;

        // Start is called before the first frame update
        void Start()
        {
            prevOrientation = Screen.orientation;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        void OnDestroy()
        {
            Screen.orientation = prevOrientation;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.deviceOrientation == DeviceOrientation.Portrait
                || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                landscapeMode.SetActive(false);
                portraitMode.SetActive(true);
            }
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft
                || Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                landscapeMode.SetActive(true);
                portraitMode.SetActive(false);
            }
        }
    }
}
