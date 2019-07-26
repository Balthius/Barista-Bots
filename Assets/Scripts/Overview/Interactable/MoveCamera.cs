using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    Vector3 direction = new Vector3(0,0,-10);
    float speed = .3f;
	// Update is called once per frame
	void Update ()
    {
            GetInput();
            Move();
    }

    private void GetInput()
    {
        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1))
            {
                return;
            }
            else
            {
                float directionX = Input.GetAxisRaw("Horizontal");
                float directionY = Input.GetAxisRaw("Vertical");
                float directionZ = 0;
                direction = new Vector3(directionX, directionY, directionZ);
            }
        }
        else
        { 
            direction = new Vector3(0, 0, 0);
        }  
    }

    public void Move()
    {
        transform.position += direction.normalized * speed;
    }
}
//Add touch and drag