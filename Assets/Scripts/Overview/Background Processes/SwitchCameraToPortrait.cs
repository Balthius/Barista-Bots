using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraToPortrait : MonoBehaviour {

    [SerializeField] GameObject portraitMode;
    [SerializeField] GameObject landScapeMode;
    private Vector3 cameraOffset = new Vector3(0,0,-10);
	// Update is called once per frame


    private void Start()
    {
        SetAutoRotate();
    }

    void Update ()
     {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            transform.position = portraitMode.transform.position + cameraOffset;
            
            //transform.rotation = new Quaternion(0,0,90,1);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            transform.position = landScapeMode.transform.position + cameraOffset;
            
            //transform.rotation = new Quaternion(0,0,0,1);
            
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            
            transform.position = portraitMode.transform.position + cameraOffset;
            
            //transform.rotation = new Quaternion(0,0,-90,1);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            
            transform.position = landScapeMode.transform.position + cameraOffset;
            
            //transform.rotation = new Quaternion(0,0,180,1);
        }
    }
    private static void SetAutoRotate()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
    
        Screen.autorotateToPortrait = true;

        Screen.autorotateToPortraitUpsideDown = false;

        Screen.autorotateToLandscapeLeft = true;

        Screen.autorotateToLandscapeRight = false;
    }

}
