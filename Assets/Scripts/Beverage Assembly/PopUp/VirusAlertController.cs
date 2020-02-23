﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusAlertController : MonoBehaviour
{

    [SerializeField] private GameObject virusOverlayPanel;

    [SerializeField] private GameObject[] popUpAd;

    public int minPopX, maxPopX, minPopY, maxPopY;
    private int virusChildCount;

    public bool virusAlertStatus;

    private void Start()
    {

        virusOverlayPanel.SetActive(false);
        StartCoroutine("CheckChildren");
        StartCoroutine("CreatePopUp");
    }

    void Update()
    {

        virusChildCount = this.gameObject.transform.childCount;

        if (virusAlertStatus)
        {
            if (Input.deviceOrientation == DeviceOrientation.Portrait)
            {
                virusOverlayPanel.SetActive(false);
            }
            if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft)
            {
                virusOverlayPanel.SetActive(true);
            }
            if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
            {
                virusOverlayPanel.SetActive(false);
            }
            if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
            {
                virusOverlayPanel.SetActive(true);
            }
        }
        else
        {
            virusOverlayPanel.SetActive(false);
        }
    }

    IEnumerator CheckChildren()
    {
        if (virusChildCount >= 5)
        {
            virusAlertStatus = true;
        }
        else
        {
            virusAlertStatus = false;
        }
        yield return new WaitForSeconds(3);
        StartCoroutine("CheckChildren");
    }

    IEnumerator CreatePopUp()
    {
        int x = Random.Range(minPopX, maxPopX);

        int y = Random.Range(minPopY, maxPopY);

        int z = Random.Range(0, popUpAd.Length);

        Vector3 objPos = new Vector3(x, y, 0);
        GameObject popUp = Instantiate(popUpAd[z], objPos, Quaternion.identity);
        popUp.transform.parent = this.transform;
        popUp.transform.position = this.transform.position + objPos;
        popUp.transform.Rotate(0,0,90);

        yield return new WaitForSeconds(1);
        StartCoroutine("CreatePopUp");
    }
}
