using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutoSortLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetComponent<SpriteRenderer>().sortingOrder = ((int)transform.position.y)*-1;	
	}
}
