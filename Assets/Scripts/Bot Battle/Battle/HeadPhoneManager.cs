using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadPhoneManager : MonoBehaviour

{
    private bool gameWon;
    private bool gameLost;

    [SerializeField] private Text scorePanelText;

    [SerializeField] private Sprite[] barSprites;
    private float currentScore;
    [SerializeField] private float totalSize;
    private int barVal;
    public int BarVal
    {
        get
        {
            return this.barVal;
        }
        set
        {
            if(value > barSprites.Length)
            {
                barVal = barSprites.Length-1;
            }
            if (value < 0)
            {
                barVal = 0;
            }
            else
            {
                barVal = value;
            }
        }
       }
    float spriteVal;

    public float CurrentScore
    {
        get
        {
            return this.currentScore;
        }

        set
        {
            if (value > totalSize)
            {
                currentScore = totalSize;
            }
            if (value < 0)
            {
                currentScore = 0;
            }
            else
            {
                currentScore = value;
            }
        }
    }

    private void Update()
    {
        SpriteRenderer headphoneSprite = this.gameObject.GetComponent<SpriteRenderer>();

        int BarVal = (int)(CurrentScore/spriteVal);
        
        if(CurrentScore < 0)
        {

            headphoneSprite.sprite = barSprites[0];
            gameLost = true;
            scorePanelText.text = "You've Lost";

        }

        if (CurrentScore >= totalSize)
        {

            headphoneSprite.sprite = barSprites[11];// I don't really want this to be hard coded
            gameWon = true;

            scorePanelText.text = "You've Won";
        }
        else
        {
            headphoneSprite.sprite = barSprites[BarVal];
        }

    }
    private void Start()
    {
        SetValues();
        gameWon = false;
        gameLost = false;
    }

    private void SetValues()
    {
        spriteVal = totalSize / barSprites.Length;
        totalSize = 100;
        CurrentScore = totalSize / 2;
    }

    public void GetValue(int result)
    {
        CurrentScore += result;
    }


}
