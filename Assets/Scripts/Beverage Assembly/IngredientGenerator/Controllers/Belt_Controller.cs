using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Belt_Controller : MonoBehaviour
{
    [SerializeField]
    GameObject scoreScreen;

    [SerializeField] private GameObject NewOrder;
    
    [SerializeField]
    GameObject machine;
   
    [SerializeField]
    GameObject Spawner;
    public ScoreCanvasController canvasController = null;


    public static bool gameActive;


    public static float speed = 5f;
    
    private int Success { get; set; }

    private int OrderSuccess { get; set; }
    
    private int gameDifficulty = 3;

    private void Start()
    {
        SetupGame();
    }

    private void Update()
    {
        if(!gameActive)
        {
            scoreScreen.SetActive(true);
        }
    }

    public void SetupGame()
    {
        gameActive = true;
        scoreScreen.SetActive(false);
        CreateOrder();

    }

    public void MoveOrders() // Sent from Machine obj
    {
        if (Belt_Controller.gameActive == true)
        {
            CreateOrder();
        }
    }
    public void CheckGameStatus()
    {
        //Used in ClockController on Gameobject basecanvas
                TallyScore();
                gameActive = false;
    }

    private void CreateOrder()
    {
        GameObject genNewOrder = Instantiate(NewOrder, Spawner.transform.position, Quaternion.identity);
        genNewOrder.GetComponent<OrderGenerator>().CreateDrinks(gameDifficulty); // Difficulty
    }
    
    public void AddSuccess()
    {
        Success++;
    }

    public void AddOrderSuccess()
    {
        OrderSuccess++;
    }
    public void TallyScore()
    {
        scoreScreen.SetActive(true);
        int totalSuccesses = Success / 3;
        int finalScore = Mathf.Clamp(totalSuccesses, 1,5);

        canvasController.PushScore(finalScore);

    }
}
