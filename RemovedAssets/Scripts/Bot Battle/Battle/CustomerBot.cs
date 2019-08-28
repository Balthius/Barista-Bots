using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerBot : Bot
{
    [SerializeField]int[] personalityPercents;
    private int[] personalityChances;
    [SerializeField]public int currentPersonality;

    [SerializeField] Sprite[] moodSprites;

    [SerializeField] Text custText;

    [SerializeField] private string[] aloofConversationLines;
    [SerializeField] private string[] madConversationLines;
    [SerializeField] private string[] sadConversationLines;
    [SerializeField] private string[] chattyConversationLines;


    //[SerializeField] private AudioClip botSound;


    override protected void Start()
    {
        //AudioManager.instance.PlaySingle(botSound);
        SetChances();
        base.Start();
        RollMood();
    }

    private void SetChances()
    {
        personalityChances = new int[personalityPercents.Length+1];
        personalityChances[0] = 0;
        personalityChances[personalityChances.Length-1] = 100;
        for (int i = 1; i < personalityPercents.Length; i++)
        {
            personalityChances[i] += personalityPercents[i] + personalityChances[i-1];
        }
    }

    public void SetMood(int moodNum)
    {
        currentPersonality = moodNum;
        PushMoodToUI();
    }

    public void RollMood()
    {
        PickMood(personalityChances);
        PushMoodToUI();
    }

    private void PushMoodToUI()
    {
        MoodManager mood = FindObjectOfType<MoodManager>();
        mood.UpdateMood(currentPersonality);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = moodSprites[currentPersonality];
        custText.text = DisplayText(currentPersonality);
    }

    private string DisplayText(int pers)
    {
        
        switch (pers)
        { 
            
            case 0:
                int i =Random.Range(0, aloofConversationLines.Length);
                return aloofConversationLines[i];
            case 1:
                int j = Random.Range(0, madConversationLines.Length);
                return madConversationLines[j];
            case 2:
                int k = Random.Range(0, sadConversationLines.Length);
                return sadConversationLines[k];
            case 3:
                int l = Random.Range(0, chattyConversationLines.Length);
                return chattyConversationLines[l];
            default:
                return ("Unnacceptable mood used");
               


        }
    }

    private void PickMood(int[] chance)
    {
        int randomRoll = Random.Range(0, 101);
        if (randomRoll < chance[1])
        {
            currentPersonality = 0;
        }
        else if (chance[1]  < randomRoll && randomRoll <= chance[2])
        {
            currentPersonality = 1;
        }
        else if (chance[2] < randomRoll && randomRoll <= chance[3])
        {
            currentPersonality = 2;
        }
        else if (chance[3] < randomRoll && randomRoll <= chance[4])
        {
            currentPersonality = 3;
        }
        else
        {
            Debug.Log("Wrong Mood");
        } 
        
    }

}
