using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonManager : MonoBehaviour
{
    [SerializeField] GameObject cafeButton;
    //[SerializeField] GameObject resetButton;

    private void Awake()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == "Cafe")
        {
            cafeButton.SetActive(false);
            //resetButton.SetActive(false);
        }
        if (activeScene.name == "Beverage Pickup" || activeScene.name == "Customer Service" || activeScene.name == "Barista Beverage")
        {
            cafeButton.SetActive(true);
            //resetButton.SetActive(true);
        }
    }
}
