using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SucceedManager : MonoBehaviour
{ private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<ArmController>() != null  && other.gameObject.tag == "FullHand")
        {
            DrinkDrunk();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

    public delegate void DrinkGrabbed();
    public static event DrinkGrabbed DrinkDrunk;
}
