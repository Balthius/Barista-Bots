using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyController : MonoBehaviour
{
    Transform dishPit;
    GameObject gameManager;
    [SerializeField] private Sprite[] dishSprites;
    

    int dirtyLevel;

    private void Start()
    {
        dishPit = GameObject.FindGameObjectWithTag("DishPit").transform;
        
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }
    private void Update()
    {
        if (dirtyLevel < 100 && dirtyLevel > 50)
        {
            this.GetComponent<SpriteRenderer>().sprite = dishSprites[0];
        }
        else if (dirtyLevel < 50 && dirtyLevel > 1)
        {
            this.GetComponent<SpriteRenderer>().sprite = dishSprites[1];
        }
      
    }
    private void OnMouseDown()
    {
        if (DishPitManager.dishActive == false)
        {
            transform.position = dishPit.position;
            DishPitManager.dishActive = true;
            dirtyLevel = 100;
        }
    }

    private void OnMouseDrag()
     {
         //clean amount could be on a random range to simulate different amounts of "dirty" per dish.
        if (dirtyLevel > 0)
        {
            dirtyLevel -= 3;
            if (dirtyLevel <= 0)
            {
                GameManager gm = gameManager.GetComponent<GameManager>();
                Debug.Log(gm.cleanDishCount + "Clean dishes and cups before " + gm.cleanCupCount);
                
                if(gameObject.tag == "Cup")
                {
                    gm.cleanCupCount++;
                }
                else if(gameObject.tag == "Plate")
                {
                    gm.cleanDishCount++;

                }

                Debug.Log(gm.cleanDishCount + "Clean dishes and cups " + gm.cleanCupCount);
                DishPitManager.dishActive = false;
                Destroy(this.gameObject);
            }
        }
    }
}
