using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int reverseAtY = -1000;
    [SerializeField] private int destroyAtY = 2000;//control destroyer
    [SerializeField] float yMovement, reverseMovement;
    private bool hasCollided = false;

    public bool hasObj = false, hasCup = false;
    public bool hasHit;
    private bool emptyHanded = true;


    // Update is called once per frame
    private void Start() 
    {
        hasHit = false;
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
    
        if(!hasHit && transform.position.y <= reverseAtY)
        {
            Debug.Log("Reverse");
            hasHit = true;
            emptyHanded = false;
           // errant hands keep getting past the wall when using trigger checks
           ObjectGrabbedCheck(false);
        }
        if (transform.position.y >= destroyAtY)
        {
            GameObject.Destroy(this.gameObject);
        }

    }

    
   
    private void OnTriggerEnter2D(Collider2D other) 
    {  
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
        Debug.Log("Grabbed something" + emptyHanded);
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
