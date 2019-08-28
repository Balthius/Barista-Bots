using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    // Start is called before the first frame update
   
    [SerializeField] float yMovement, reverseMovement;
    private bool hasCollided = false;

    public bool hasObj = false, hasCup = false;
    public GameObject hasHit;
    private bool emptyHanded = true;


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

    
   
    private void OnTriggerEnter2D(Collider2D other) 
    {  
        hasHit = other.gameObject;// used to debug
         if(!hasCollided)
        { 
            hasCollided = true;
            CupController otherCup = other.gameObject.GetComponent<CupController>();
            if(otherCup != null && otherCup.combined && !hasObj)
            {
                ObjectGrabbedCheck(true);
                GameManager gm = FindObjectOfType<GameManager>();
                gm.CheckToRefill();
                other.transform.parent = this.gameObject.transform;
                
            Destroy(other.gameObject.GetComponent<BoxCollider2D>());
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
            hasCup = false;
            
        }
        else if(grabbedCup == true)
        {
            GetComponent<Animator>().SetTrigger("GrabCup");
            hasObj = true;
            hasCup = true;
        }
    }
    
}
