using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsColliding : MonoBehaviour
{
    private Vector3 origSize;
    
    private void Start()
    {
        origSize = this.gameObject.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.transform.localScale = new Vector3(.9f,.9f,.9f);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.transform.localScale = origSize;
    }
}
