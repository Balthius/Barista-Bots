using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyController : MonoBehaviour
{

    [SerializeField] Transform dishPit;

    [SerializeField] GameObject[] cleanDish;//0 Cup 1 Dish

    [SerializeField] private Sprite[] dishSprites;
    [SerializeField] GameManager gameManager;

    int dirtyLevel;
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
        }
    }

    private void OnMouseDrag()
     {
        if (dirtyLevel > 0)
        {
            dirtyLevel -= 5;
            if (dirtyLevel <= 0)
            {
                GameManager gm = gameManager.GetComponent<GameManager>();
                if(gameObject.tag == "Cup")
                {
                    gm.cleanCups++;
                    gm.CreateCleanObj(cleanDish[0]);
                }
                else if(gameObject.tag == "Dish")
                {
                    gm.cleanDishes++;
                    gm.CreateCleanObj(cleanDish[1]);

                }
                    Destroy(this.gameObject);
            }
        }
    }
}
