using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombinedDishController : MonoBehaviour
{  
     private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Arm" && gameObject.tag == "Combined")
        {
            other.tag = "FullHand";

            GetComponent<BoxCollider2D>().enabled = false;
            Destroy(GetComponent<Rigidbody2D>());
        }
    }
    
}
