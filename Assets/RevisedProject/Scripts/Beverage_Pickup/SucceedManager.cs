using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SucceedManager : MonoBehaviour
<<<<<<< HEAD

{ 
    
    [SerializeField]GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D other)
=======
{ private void OnTriggerEnter2D(Collider2D other)
>>>>>>> Beverage_Pickup
    {

        ArmController arm = other.gameObject.GetComponent<ArmController>();
        if(arm != null  && arm.hasCup)
        {
            SuccessEvent();
        }
<<<<<<< HEAD
        else if(arm != null  && !arm.hasCup)
        {
            gameManager.GetComponent<GameManager>().RemoveLife();
        }
=======
>>>>>>> Beverage_Pickup
        Destroy(other.gameObject);
    }

    public delegate void DrinkGrabbed();
    public static event DrinkGrabbed SuccessEvent;
}
