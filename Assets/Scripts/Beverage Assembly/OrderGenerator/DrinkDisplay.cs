using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject[] ingredientObj;    
    
    [SerializeField]
    private Sprite cleared;

    public bool emptySpot = true;

    bool started = false;


    [SerializeField] private AudioClip clearedClip;


    public void AssignDrinks(Sprite[] ingredients)
        {
            for (int i = 0; i < ingredients.Length; i++)
            {
            SpriteRenderer spriteObj = ingredientObj[i].GetComponent<SpriteRenderer>();
            spriteObj.sprite = ingredients[i];
            }
        }
    
    public void CheckIngredient(string name, GameObject sentIngredient)
    {
        Ingredient sentIng = sentIngredient.GetComponent<Ingredient>();
        for (int i = 0; i < ingredientObj.Length; i++)
        {
            SpriteRenderer spriteObj = ingredientObj[i].GetComponent<SpriteRenderer>();
            if (spriteObj.sprite != null && name == spriteObj.sprite.name)
            {
                Debug.Log("Cup has cleared");
                AudioManager.instance.PlaySingle(clearedClip);
                spriteObj.sprite = null;
                Destroy(sentIngredient);
                break;
            }
            else
            {
                sentIng.StartCoroutine("ReturnIngr");
            }
        }
        AllCleared();
    }
    private void AllCleared()
    {
        int x = 0;
        for(int i = 0; i < ingredientObj.Length; i++)
        {
            SpriteRenderer spriteObj = ingredientObj[i].GetComponent<SpriteRenderer>();
            if (spriteObj.sprite == null)
            {
                x++;
                if(x == ingredientObj.Length)
                {
                    Debug.Log("Checking all cleared");
                    Belt_Controller beltObj = GameObject.FindGameObjectWithTag("Belt").GetComponent<Belt_Controller>();
                    beltObj.GetComponent<Belt_Controller>().AddSuccess();
                    FindObjectOfType<OrderGenerator>().FinishedOrder();
                    Destroy(this.gameObject);
                }
            }
            else
            {
                x = 0;
                break;
            }
        }
    }
}
