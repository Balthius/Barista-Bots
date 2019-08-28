using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm_Obj : MonoBehaviour {
// Commented things out to fix error codes
    //public GameManager gc;
    private Transform position;
    public Sprite secondArm;
    private bool goingUp;
	// Use this for initialization
	void Start () {
        position = this.transform;
        float random = Random.Range(-8.0f, 8.0f);
        position.Translate(new Vector2(random, 0));
        goingUp = false;
	}
	
	// Update is called once per frame
	void Update () {

       /*  if (gc.missedCustomer() == 3)
        {
            Destroy(this.gameObject);
        } */

        if (goingUp == false)
        {
            position.Translate(new Vector2(0f, -0.03f));
        }
        else
        {
            position.Translate(new Vector2(0f, 0.06f));
        }
       
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "white_rectangle_bottom")
        {
            position.Translate(new Vector2(0f, -2.67f));
            this.GetComponent<SpriteRenderer>().sprite = secondArm;
            goingUp = true;
            //gc.hitBottom();
        }
        else if (collision.tag == "Combined")
        {
            collision.transform.position = transform.position;
            collision.transform.parent = transform;
            collision.transform.Translate(new Vector3(0, -7f, 0));
            goingUp = true;
           /*  gc.placeObj = true;
            gc.placeItem("cleanPlate");
            gc.placeItem("dirtyPlate");
            gc.placeItem("cleanCup");
            gc.placeItem("dirtyCup");
           
            if (gc.cups == 0)
            {
                gc.noCups = true;
            } */
        }
        else
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "white_rectangle_top")
        {
            Destroy(this.gameObject);
        }
    }
}
