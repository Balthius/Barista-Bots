using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot_Page : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        Debug.Log("clicked image");
        //this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
