using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SwipeDetectionOnUIElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action<float, float> OnSwipeDetected;

    Vector2 pressPosition;

    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<Image>().raycastTarget)
        {
            Debug.LogError("SwipeDetectionOnUIElement: raycastTarget required for Image");
        }
    }
    
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        pressPosition = eventData.position;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        Vector2 releasePosition = eventData.position;
        float deltaX = releasePosition.x - pressPosition.x;
        float deltaY = releasePosition.y - pressPosition.y;

        OnSwipeDetected(deltaX, deltaY);
    }
}