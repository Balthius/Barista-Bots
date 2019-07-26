using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPage : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TriggerSwitch()
    {
        transform.parent.parent.GetComponent<CardControl>().OnActivate(); ;
    }
}
