using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour {

    private void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {

        FindObjectOfType<Belt_Controller>().SetupGame();

        FindObjectOfType<OrderGenerator>().OverrideDestroy();

        Time.timeScale = 1;
    }
}
