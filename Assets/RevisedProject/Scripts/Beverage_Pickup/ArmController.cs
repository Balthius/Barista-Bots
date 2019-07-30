using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField] float yMovement, reverseMovement;
    private bool hasCollided = false;

    public bool hasObj = false;
    public GameObject hasHit;

    private bool emptyHanded = true;

    private float timeExisted;

    // Update is called once per frame
    private void Start() 
    {
        reverseMovement = (yMovement * -1) * 1.3f;
    }
    void Update()
    {
        if(emptyHanded)
        {
            transform.Translate(new Vector2(0,yMovement));
        }
        else if(!emptyHanded)
        {
            transform.Translate(new Vector2(0,reverseMovement));
        }
    
        if(hasHit != null && hasHit.gameObject.name == "FailWall")
        {
            
            emptyHanded = false;
           // errant hands keep getting past the wall when using trigger checks
           ObjectGrabbedCheck(false);
        }

    }

    
    public delegate void DrinkGrabbed();
    public static event DrinkGrabbed DrinkDrunk;
    private void OnTriggerEnter2D(Collider2D other) 
    {  
        hasHit = other.gameObject;// used to debug
         if(!hasCollided)
        { 
            hasCollided = true;
            
            if(other.gameObject.tag == "Combined" && !hasObj)
            {
                ObjectGrabbedCheck(true);
                GameManager gm = FindObjectOfType<GameManager>();
                gm.CheckToRefill();
            }
            else
            {
               emptyHanded = true; 
            }
             
        }
    }

    public void ObjectGrabbedCheck(bool grabbedCup)
    {  
        
        emptyHanded = false;
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
        }
    }
     private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.name == "SucceedWall" && gameObject.tag == "FullHand")
        {
            DrinkDrunk();
            Destroy(this.gameObject);
        }
    }
}
