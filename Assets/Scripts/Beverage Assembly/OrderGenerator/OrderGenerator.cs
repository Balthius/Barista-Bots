using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderGenerator : MonoBehaviour
{
    private int drinksComplete;
    public int drinksSpawned;

    [SerializeField] private GameObject[] drinkSpawnSpots;
    
    private int totalDrinks;
    
    private int CompletedDrinks = 0;

    private float step;

    [SerializeField] private float beltSpeed;

    private bool drinksPlaced= false;

    private void Start()
    {
        //QuickMoveOrder();
        drinksPlaced = false;
        StartCoroutine("DrinksPlacedStatus");
        //StartCoroutine("CheckSpeed");
    }

    IEnumerator CheckSpeed()
    {
        Beverage_Assembly_ControlPanels panelCon = FindObjectOfType<Beverage_Assembly_ControlPanels>();

        beltSpeed = panelCon.gameSpeed;
        yield return new WaitForSeconds(4);
        StartCoroutine("CheckSpeed");
        //

        Debug.Log(beltSpeed + " Is belt speed");
    }
    
    private void Update()
    {
        if(!drinksPlaced)
        {
            SlowMoveDrinks();
        }
    }

    private void SlowMoveDrinks()
    {
        step = Time.deltaTime * 1200;
        Debug.Log(step);

        GameObject activeOrderSpot = GameObject.FindGameObjectWithTag("ActiveOrder");
        Vector3 target = activeOrderSpot.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
    private void QuickMoveOrder()
    {
        GameObject activeOrderSpot = GameObject.FindGameObjectWithTag("ActiveOrder");
        this.transform.position = activeOrderSpot.transform.position;
    }

    public void CreateDrinks(int difficulty)
    {
        totalDrinks = difficulty + 1;
        for(int i = 0; i < totalDrinks; i++)
        {
            drinkSpawnSpots[i].GetComponent<DrinkGenerator>().SpawnDrink();
        }
    }
   
    public void FinishedOrder()
    {
        CompletedDrinks++;


        if(totalDrinks == CompletedDrinks)
        {
            Belt_Controller beltObj = GameObject.FindGameObjectWithTag("Belt").GetComponent<Belt_Controller>();
            beltObj.GetComponent<Belt_Controller>().AddOrderSuccess();//An extra success added for completing the order

            GameObject destroyer = GameObject.FindGameObjectWithTag("Destroyer");
            this.transform.position = Vector3.Lerp(this.transform.position, destroyer.transform.position, step);
            FindObjectOfType<Beverage_Assembly_ControlPanels>().SpawnNewOrder();
        }
    }

    public void OverrideDestroy()
    {
        Debug.Log("DestroyMe");
        Destroy(this.gameObject);
    }

    private IEnumerator DrinksPlacedStatus()
    {
        yield return new WaitForSeconds(3);
        drinksPlaced = true;
    }
}
