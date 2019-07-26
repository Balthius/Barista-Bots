using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupController : MonoBehaviour
{

    [SerializeField]Sprite[] cupSprites;

    [SerializeField] GameObject dishObj;
    
    private bool isPickedUp = false;
    private Vector3 screenPoint;
    private Vector3 offset;
    //CupMovement
    void OnMouseDown()
    {
        if (isPickedUp == false && Input.touchCount == 1)
        {
            isPickedUp = true;
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
    }

    private void OnMouseUp()
     {
        isPickedUp = false;
    }

    void OnMouseDrag()
    {
        if (isPickedUp == true && Input.touchCount == 1)
        {
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
        if(other.tag == "Plate" && this.tag != "Combined")
        {
            Destroy(other.gameObject);
            GameObject dish = Instantiate(dishObj, transform.position, Quaternion.identity);
            transform.tag = "Combined";
            dish.transform.parent = this.gameObject.transform;
        }
        if(other.tag == "Arm" && gameObject.tag == "Combined")
        {
            transform.parent = other.transform;
        }
    }
}
