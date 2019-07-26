using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSlot : MonoBehaviour {

    [SerializeField]
    private GameObject ingredient;


    public void SpawnIngredient()
    {
        GameObject newingredient = Instantiate(ingredient, transform.position, Quaternion.identity);
        newingredient.transform.parent = this.gameObject.transform;
    }
    
}
