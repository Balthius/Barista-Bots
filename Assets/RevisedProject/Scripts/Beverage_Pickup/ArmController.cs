using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField] float yMovement;
    private bool hasCollided = false;

    public bool hasObj = false, hasCup = false;
    public GameObject hasHit;

    // Update is called once per frame
    void Update()
    {
            transform.Translate(new Vector2(0,yMovement));
    }

    
    public delegate void DrinkGrabbed();
    public static event DrinkGrabbed DrinkDrunk;
    private void OnTriggerEnter2D(Collider2D other) 
    {  
        hasHit = other.gameObject;
         if(!hasCollided)
        {
            
            hasCollided = true;
            if(other.gameObject.tag == "Wall")
            {
                ObjectGrabbedCheck(false);
            yMovement *= -1;
            }
                 if(other.gameObject.tag == "Combined" && !hasObj)
            {
                ObjectGrabbedCheck(true);
            yMovement *= -1;
            }
             
        }
    }

    public void ObjectGrabbedCheck(bool grabbedCup)
    {  
        
        if(grabbedCup == false)
        {
            GetComponent<Animator>().SetTrigger("GrabCutlery");
            hasObj = true;
            transform.tag = "FullHand";
        }
        else if(grabbedCup == true)
        {
            GetComponent<Animator>().SetTrigger("GrabCup");
            hasObj = true;
            hasCup = true;

        }
    }
     private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Destroyer" && hasObj)
        {
            DrinkDrunk();
            Destroy(this.gameObject);
        }
        
    }
}
