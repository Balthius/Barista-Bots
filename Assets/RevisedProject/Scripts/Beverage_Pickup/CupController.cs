using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{

    [SerializeField]Sprite[] cupSprites;

    [SerializeField] GameObject dishObj;
    
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool playerTouching = false;
    public bool combined = false;

    public bool beingHeld = false;
    //CupMovement

    private void Update() {
    
            //Debug.Log("is picked up" + isPickedUp);
    }
    void OnMouseDown()
    {// && Input.touchCount == 1 removed to test
        if (!playerTouching && !beingHeld)
        {
            playerTouching = true;
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseUp()
    {           

        playerTouching = false;
      
    }

    void OnMouseDrag()
    {// && Input.touchCount == 1
        if (playerTouching == true)
        {

            //Mouse doesnt count as a touchcount for testing
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
            
        }
    }
    private void Start()
    {
        SetCup();
    }   
    private void SetCup()
    {
            int i = Random.Range(0, cupSprites.Length);
            GetComponent<SpriteRenderer>().sprite = cupSprites[i];
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<DishController>() != null && !combined)
        {
            Destroy(other.gameObject);
            GameObject dish = Instantiate(dishObj, transform.position, Quaternion.identity);
            dish.transform.parent = this.gameObject.transform;
            combined = true;
            playerTouching = false;//Backup bool check
            this.gameObject.name = "CombinedBeverage";
        }
    }
}
