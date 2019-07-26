using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine_Controller : MonoBehaviour
{
    [SerializeField]
    protected GameObject[] ingredientSpots;

    [SerializeField]
    protected GameObject spawnableIngredient;

    protected bool onCooldown;

    [SerializeField]
    protected int cooldownTimer;
    // Use this for initialization

    [SerializeField]
    int simultaneousIngr;

    [SerializeField] protected GameObject ingTimer;

    protected void OnMouseDown()
    {
        if (Belt_Controller.gameActive == true)
        {
            SpawnOnClick();
        }
    }
    
    protected void SpawnOnClick()
    {
        if(!onCooldown)
        {
            ingTimer.GetComponent<Cooldown>().BeginCountdown(cooldownTimer); // Send info to Cooldown to change display
            SpawnTimer(); // Sets if ingredient is on cooldown
        }
    }
    
    protected void SpawnTimer()
    {
        // Why is this a seperate method?
        StartCoroutine("InternalCooldown");
    }

    protected IEnumerator InternalCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        SpawnIngredient();
        onCooldown = false;
    }
    public void SpawnOnTimerComplete()
    {
        //Used by Cooldown
        SpawnIngredient();
    }

    protected void SpawnIngredient()
    {
        int x = 0;
        for (int i = 0; i < ingredientSpots.Length; i++)
        {

            //Debug.Log("Child count " + ingredientSpots[i].gameObject.transform.childCount);
            if (ingredientSpots[i].gameObject.transform.childCount < 1 && x < simultaneousIngr)
            {
                GameObject ingredient = Instantiate(spawnableIngredient, ingredientSpots[i].transform.position, Quaternion.identity);
                ingredient.transform.SetParent(ingredientSpots[i].transform);
                x++;

                //Debug.Log("Child count after " + ingredientSpots[i].gameObject.transform.childCount);
            }
            else
            {
                //Debug.Log("Order is full for " + this.gameObject.name + ingredientSpots[i].gameObject.name);
            }
        }
    }
}
