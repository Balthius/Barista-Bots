using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour
{
    private bool isPickedUp = false;
    private Vector3 screenPoint;
    private Vector3 offset;


    void OnMouseDown()
    {
        if (isPickedUp == false && Input.touchCount == 1)
        {
            isPickedUp = true;
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseUp()
     {
        isPickedUp = false;
    }

    void OnMouseDrag()
    {
        if (isPickedUp == true && Input.touchCount == 1)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

 
}
