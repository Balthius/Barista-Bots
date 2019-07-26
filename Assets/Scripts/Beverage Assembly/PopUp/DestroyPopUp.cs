using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPopUp : MonoBehaviour {
    private void OnMouseDown()
    {
        this.GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    private void OnMouseUp()
    {
        Destroy(transform.parent.gameObject);
        //Debug.Log("Parent Detected");
    }
}
