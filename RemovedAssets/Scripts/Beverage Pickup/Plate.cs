using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour {
    private Vector3 screenPoint;
    private Vector3 offset;
    public Sprite dirtySprite1;
    public Sprite dirtySprite2;
    public Sprite dirtySprite3;
    public bool isPickedUp;
    private bool isDragging;
    public bool isDirty;
    private float dirtyLevel;
    public GameController gc;

    // Use this for initialization
    void Start () {
        isPickedUp = false;
        isDragging = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (gc.missedCustomer() == 3)
        {
            Destroy(this.gameObject);
        }
        if (dirtyLevel == 0)
        {
        
        }
        else if (dirtyLevel < 50 && dirtyLevel > 25)
        {
            this.GetComponent<SpriteRenderer>().sprite = dirtySprite2;
        }
        else if (dirtyLevel < 25)
        {
            this.GetComponent<SpriteRenderer>().sprite = dirtySprite3;
        }
	}

    void OnMouseDown()
    {
        if (isPickedUp == false && Input.touchCount == 1 && isDirty == false)
        {
            Debug.Log("Picked Up");
            screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }
        else if (isDirty == true)
        {
            transform.position = new Vector3(18.05f,0.05f,0);
            transform.localScale = new Vector3(1f, 1f, 0f);
        }
       
    }

    public void setDirty(bool dirty, float level)
    {
        isDirty = dirty;
        dirtyLevel = level;
    }

    public bool getDirty()
    {
        return isDirty;
    }

    void OnMouseDrag()
    {
        if (isPickedUp == false && Input.touchCount == 1 && isDirty == false)
        {
            isDragging = true;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }
        else if (isDirty == true)
        {
            if (dirtyLevel > 0)
            {
                dirtyLevel -= 1f;
                if (dirtyLevel <= 0)
                {
                    gc.cleanedPlate();
                    gc.placeItem("cleanPlate");
                    Destroy(this.gameObject);
                }
            }
        }
        
    }
    private void OnMouseUp()
    {
        isDragging = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cup" && isDragging == true)
        {
            collision.transform.position = transform.position;
            collision.transform.parent = transform;
            collision.gameObject.GetComponent<Cup>().isPickedUp = true;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.transform.Translate(new Vector3(0.3f, 0, 0));
            transform.tag = "Combined";

        }
        
    }
}
