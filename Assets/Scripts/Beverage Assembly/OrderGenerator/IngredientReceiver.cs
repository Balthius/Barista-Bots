using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientReceiver : MonoBehaviour {

    [SerializeField]
    private GameObject drinkDisplay;
    private void OnTriggerEnter(Collider other)
    {

        //Debug.Log(this.gameObject.name + "triggering on" + other.gameObject);
        Sprite ingredientImage = other.gameObject.GetComponent<SpriteRenderer>().sprite;
        DrinkDisplay order = drinkDisplay.GetComponent<DrinkDisplay>();
        order.CheckIngredient(ingredientImage.name, this.gameObject);//Do I use this at all?
        Destroy(other.gameObject);
    }
}
