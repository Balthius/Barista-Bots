using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
  protected ServiceBattle serviceBattle;
    
    // Use this for initialization
    virtual protected void Start ()
    {
        serviceBattle = GameObject.FindObjectOfType<ServiceBattle>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    protected virtual void ActiveBot()
    {
   
    }

}
