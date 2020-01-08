﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CafeInputDetection : MonoBehaviour
{
    public CafeGameManager manager;
    public Camera mainCamera;
    public float swipeTimeThreshold = 100f;

    private float tapDuration = 0;
    private string currentBotDisplaying = null;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                tapDuration = Time.time;
                if (currentBotDisplaying == null)
                {
                    manager.OnPress();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (Time.time - tapDuration < 0.5f)
                {
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

                    string name = hit.transform.name;

                    if (name != "GameManager")
                    {
                        if (name == "Shae_Collider" || name == "Pia_Collider" || name == "CeCe_Collider")
                        {
                            if (currentBotDisplaying == null)
                            {
                                hit.transform.GetComponentInParent<Main_Cafe_Bot>().OnTap();
                            }
                        }
                        else {
                            if (currentBotDisplaying == null || currentBotDisplaying == name)
                            {
                                CafeSpot spot = hit.transform.GetComponentInParent<CafeSpot>();
                                if (spot != null)
                                {
                                    spot.OnTap();
                                }

                                currentBotDisplaying = !spot.data.isEmpty && currentBotDisplaying == null ? name : null;
                            }
                        }
                    }
                }
            }
            else {
                manager.OnDrag();
            }
        }
    }
}