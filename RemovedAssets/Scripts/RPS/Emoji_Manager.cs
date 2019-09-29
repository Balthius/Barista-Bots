using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emoji_Manager : MonoBehaviour {

    [SerializeField] private GameObject[] faceArray;

    [SerializeField] private GameObject customerBot;

    private int currentNum;

    private void Start()
    {
        currentNum = 0;
        DisplayEmoji();
    }

    public void NumberUp()
    {
        currentNum++;
        if(currentNum > 2)
        {
            currentNum = 0;
        }
        DisplayEmoji();
    }

    public void NumberDown()
    {
        currentNum--;
        if (currentNum < 0)
        {
            currentNum = 2;
        }
        DisplayEmoji();
    }

    public void SelectEmoji()
    {
        Debug.Log("Current Num is " + currentNum);
       customerBot.GetComponent<CustomerManager>().CheckRPS(currentNum);
    }

    private void DisplayEmoji()
    {
        foreach(GameObject face in faceArray)
        {
            face.SetActive(false);
        }
        faceArray[currentNum].SetActive(true);
    }
}
