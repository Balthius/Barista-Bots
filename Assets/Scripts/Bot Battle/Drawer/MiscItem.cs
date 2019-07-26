using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItem : MonoBehaviour
{
    protected Vector3 screenPoint;

    protected Vector3 offset;
    protected bool isHeld;

    protected CustomerBot customerBot;

    protected void Start()
    {
        customerBot = FindObjectOfType<CustomerBot>();
    }

    protected void OnMouseDown()
    {
        isHeld = true;
        screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);//When do we use?
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    protected void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {

    }

    protected virtual void OnMouseUp()
    {
        StartCoroutine("HeldFalse");
    }
    
    protected IEnumerator HeldFalse()
    {
        yield return new WaitForSeconds(.1f);// prevents conflicting messages on how to handle overlapped objects
        isHeld = false;
    }



}
