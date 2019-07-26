using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Page : MonoBehaviour {

    CafeBot p;
	// Use this for initialization
	void Start () {
        p = this.GetComponentInParent<CafeBot>();
	}

    private void OnMouseDown()
    {
        p.clicked = true;
    }
    // Update is called once per frame
    void Update () {
		
	}
}
