using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkGenerator : MonoBehaviour
{
    [SerializeField] Sprite[] ingredients;
    [SerializeField] GameObject[] drinks;

    public void SpawnDrink()
    {
        Sprite[] chosenIngred = new Sprite[2];
        for (int i = 0; i < chosenIngred.Length; i++)
        {
            int x = Random.Range(0, ingredients.Length);
            chosenIngred[i] = ingredients[x];
        }

        int y = Random.Range(0, drinks.Length);
        GameObject newDrink = Instantiate(drinks[y], this.transform.position, Quaternion.identity);
        newDrink.transform.SetParent(this.transform);
        DrinkDisplay newDrinkDisplay = newDrink.GetComponent<DrinkDisplay>();
        newDrinkDisplay.AssignDrinks(chosenIngred);

    }
}
