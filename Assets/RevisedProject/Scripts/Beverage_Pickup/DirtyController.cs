using UnityEngine;

public class DirtyController : MonoBehaviour
{
    Transform dishPit;
    GameManager gameManager;
    [SerializeField] private Sprite[] dishSprites;

    int dirtyLevel;

    private void Start()
    {
        dishPit = GameObject.FindGameObjectWithTag("DishPit").transform;

        gameManager = FindObjectOfType<GameManager>();
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
                Debug.Log(gameManager.cleanDishCount + "Clean dishes and cups before " + gameManager.cleanCupCount);

                if (gameObject.tag == "Cup")
                {
                    gameManager.cleanCupCount++;
                }
                else if (gameObject.tag == "Plate")
                {
                    gameManager.cleanDishCount++;
                }

                Debug.Log(gameManager.cleanDishCount + "Clean dishes and cups " + gameManager.cleanCupCount);
                DishPitManager.dishActive = false;
                Destroy(this.gameObject);
            }
        }
    }
}
