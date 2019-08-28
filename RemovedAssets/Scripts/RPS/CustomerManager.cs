using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerManager : MonoBehaviour
{
    public int botBehavior;
    public int selectedBehavior;
    public int roundsToPlay = 10;

    private SpriteRenderer spriteRenderer;
    private Text endButtonText;
    private int activeBot;
    private int playerWins;
    private int customerWins;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ResponseManager dialogManager;
    [SerializeField] private AudioClip[] clipList;
    [SerializeField] private GameObject endButton;
    [SerializeField] private Sprite[] businessBotArray;
    [SerializeField] private Sprite[] cheapskateBotArray;
    [SerializeField] private Sprite[] parentalBotArray;
    [SerializeField] private Text roundsText;
    [SerializeField] private Text playerText;
    [SerializeField] private Text customerText;
    
    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        endButtonText = endButton.GetComponentInChildren<Text>();

        activeBot = Random.Range(0, 3);
        ChangeBehavior();
        roundsText.text = "/" + roundsToPlay;
    }

    public void CheckRPS(int selection)
    {
        selectedBehavior = selection;
        botBehavior = ChangeBehavior();

        if (selectedBehavior == 0 && botBehavior == 1
            || selectedBehavior == 1 && botBehavior == 2
            || selectedBehavior == 2 && botBehavior == 0)
        {
            audioManager.PlaySingle(clipList[1]);
            customerWins++;
        }

        else if (selectedBehavior == 2 && botBehavior == 1
            || selectedBehavior == 1 && botBehavior == 0
            || selectedBehavior == 0 && botBehavior == 2)
        {
            audioManager.PlaySingle(clipList[0]);
            playerWins++;
        }

        else
        {
            audioManager.PlaySingle(clipList[1]);
        }
    }

    private int ChangeBehavior()
    {
        botBehavior = Random.Range(0, 3);
        int botPicNum = botBehavior + 1;
        switch (activeBot)
        {
            case 0: spriteRenderer.sprite = businessBotArray[botPicNum];
                dialogManager.UpdateDialog(botBehavior);
                break;

            case 1: spriteRenderer.sprite = cheapskateBotArray[botPicNum];
                dialogManager.UpdateDialog(botBehavior);
                break;

            case 2:  spriteRenderer.sprite = parentalBotArray[botPicNum];
                dialogManager.UpdateDialog(botBehavior);
                break;

            default:
                break;
        }

        StartCoroutine("ChangePic");
        return botBehavior;
    }

    private IEnumerator ChangePic()
    {
        yield return new WaitForSeconds(1);
        CheckWins();

        SpriteRenderer sr = this.gameObject.GetComponent<SpriteRenderer>();
        switch (activeBot)
        {
            case 0:
                sr.sprite = businessBotArray[0];
                break;

            case 1:
                sr.sprite = cheapskateBotArray[0];
                break;

            case 2:
                sr.sprite = parentalBotArray[0];
                break;

            default:
                break;
        }
    }

    private void CheckWins()
    {
        playerText.text = playerWins.ToString();
        customerText.text = customerWins.ToString();

        int totalWins = playerWins + customerWins;

        if(totalWins >= roundsToPlay)
        {

            if(playerWins > customerWins)
            {
                endButton.SetActive(true);
                endButtonText.text = "You Win";
            }
            else if(customerWins > playerWins)
            {

                endButton.SetActive(true);
                endButtonText.text = "You Lose";
            }
        }
    }
}
