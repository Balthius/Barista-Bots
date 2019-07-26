using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomManager : MonoBehaviour {

   
    public void Start()
    {
        ZoomFull();
    }


    public void ZoomFull()
    {
        Camera zoomControl = FindObjectOfType<Camera>();
        zoomControl.orthographicSize = 16.2f;
        transform.position = new Vector3(0, 0, -10);

    }


    public void ZoomPC()
    {
        Camera zoomControl = FindObjectOfType<Camera>();

        zoomControl.orthographicSize = 7;
    }


    public void ZoomMobile()
    {
        Camera zoomControl = FindObjectOfType<Camera>();

        zoomControl.orthographicSize = 3;
    }
}
//Switch from zoom buttons to a +/- system, as well as a pinch to zoom method