using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
   
    [SerializeField] Sprite ingType;
    private Vector3 screenPoint;

    private Vector3 offset;
    bool isHeld;

    private GameObject currentCollider;

    private bool gameActive;

    private void OnMouseDown()
    {
        Debug.Log("This object is" + this.gameObject.name);
            isHeld = true;
            screenPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);//When do we use?
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    private void OnMouseDrag()
    {
        if (Belt_Controller.gameActive == true)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {//Collisions are being detected fine
        if (collision.gameObject.CompareTag("Drink"))
        {
            this.gameObject.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
        }
        if (!isHeld)
        {
            if (collision.gameObject.CompareTag("Drink"))
            {
                Debug.Log("Checking Ingredient against drink");
                DrinkDisplay colDrink = collision.GetComponent<DrinkDisplay>();
                colDrink.CheckIngredient(ingType.name, this.gameObject);
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnMouseUp()
    {
        Debug.Log("Not Held");
        isHeld = false;
    }

    public IEnumerator ReturnIngr()
    {
        yield return new WaitForSeconds(.1f);
        transform.position = transform.parent.position;
    }

    
}
