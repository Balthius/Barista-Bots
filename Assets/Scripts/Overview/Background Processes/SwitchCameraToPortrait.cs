﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraToPortrait : MonoBehaviour {

    [SerializeField] GameObject portraitMode;
    [SerializeField] GameObject landScapeMode;
    private Vector3 cameraOffset = new Vector3(0,0,-10);
	// Update is called once per frame
	void Update () {
        if (Input.deviceOrientation == DeviceOrientation.Portrait)
        {
            transform.position = portraitMode.transform.position + cameraOffset;
            
            this.transform.rotation = Quaternion.Euler(0, 0, 90f);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
        {
            transform.position = landScapeMode.transform.position + cameraOffset;
            
            this.transform.rotation = Quaternion.Euler(0, 0, 180f);
        }
        if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
        {
            transform.position = portraitMode.transform.position + cameraOffset;
            
            this.transform.rotation = Quaternion.Euler(0, 0, 270f);
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
        {
            transform.position = landScapeMode.transform.position + cameraOffset;
            
            this.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
