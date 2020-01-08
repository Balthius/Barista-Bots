using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeBot : MonoBehaviour {

    public Vector3 screenLoc;
    public GameObject world;
    public bool clicked;
    private SpriteRenderer[] sprites;
    
	// Use this for initialization
	void Start () {
        clicked = false;
        sprites = this.GetComponentsInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (world.transform.position != screenLoc && clicked)
        {
            CafeGameManager.canMove = false;
            float step = 8 * Time.deltaTime; // calculate distance to move
            world.transform.position = Vector3.MoveTowards(world.transform.position, screenLoc, step);
        }
        else if (world.transform.position == screenLoc && clicked)
        {
            if (sprites[0].enabled == false)
            {
                Fade.dark = true;
                CafeGameManager.canMove = false;
                sprites[0].enabled = true;
                sprites[1].enabled = true;
            }
            else
            {
                Fade.dark = false;
                CafeGameManager.canMove = true;
                sprites[0].enabled = false;
                sprites[1].enabled = false;
            }
            clicked = false;
        }
        else {
            clicked = false;
        }
    }
}
