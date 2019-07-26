using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServiceBattle : MonoBehaviour
{

    private Text responseField;
    private int roundsTaken;

    [SerializeField]
    private GameObject headphoneObj;

    //[SerializeField] private GameObject reactionPanel;

    
    //[SerializeField] private AudioClip successAudio;
    //[SerializeField] private AudioClip failAudio;


    //[SerializeField] private AudioClip bgmClip;



    public static int[] bakedLevels = new int[4];

    
    private int[] aloofStrengths = { 0, 2 };

    private int[] madStrengths = { 0, 1 };

    private int[] sadStrengths = { 1, 3 };

    private int[] chillStrengths = { 2, 3 };

    [SerializeField]
    public GameObject[] responseLevelFields;
    
    private float addedNum;

    //[SerializeField] private Text modifiedNum; not active

    private void Awake()
    {
        //AudioManager.instance.StartBGM(bgmClip);
    }

    public void ResponseBattle(int battleChoice)
    {
        responseField = GameObject.Find("ResponseText").GetComponent<Text>();

        CustomerBot customerBot = FindObjectOfType<CustomerBot>();
        EmployeeBot employeeBot = FindObjectOfType<EmployeeBot>();
        int[] customerWeaknesses = SetWeaknesses(customerBot.currentPersonality);

        RollValues(CheckAdvantage(battleChoice, customerWeaknesses), bakedLevels[battleChoice], battleChoice);
        
        foreach (int x in customerWeaknesses)
        {
            Debug.Log("Value Increased is" + x + " With Personality" + customerBot.currentPersonality);
            BoostLevels(x);
        }
        ResetLevels(battleChoice);
        UpdateLevels(bakedLevels);
        customerBot.RollMood();

    }
    
    public void ModifyNumberByItem(int num, float numMul)
    {
        addedNum += num;
        addedNum = addedNum * (numMul/2);
        string stringNum = addedNum.ToString();
        //modifiedNum.text = "+" + stringNum; not active
    }

    private static bool CheckAdvantage(int battleChoice, int[] customerWeaknesses)
    {
        for (int i = 0; i < customerWeaknesses.Length; i++)
        {
            if (customerWeaknesses[i] == battleChoice)
            {
                return true;
            }
        }
        return false;
    }

    private int[] SetWeaknesses(int personality)
    {
        switch (personality)
        {

            case 0: return aloofStrengths;
               
            case 1: return madStrengths;

            case 2: return sadStrengths;

            case 3: return chillStrengths;

            default: return null;
        }
    }

    private static void BoostLevels(int weaknesses)
    {
        switch(weaknesses)
        {
            case 0:
                if (bakedLevels[0] < 3)
                { bakedLevels[0]++; }
                break;
            case 1:
                if(bakedLevels[1] < 3)
                { bakedLevels[1]++; }
                
                break;
            case 2:
                if (bakedLevels[2] < 3)
                { bakedLevels[2]++; }
                break;
            case 3:
                if (bakedLevels[3] < 3)
                { bakedLevels[3]++; }
                break;
            default:
                break;
        }
    }

    private static void ResetLevels(int chosen)
    {
        switch (chosen)
        {
            case 0:
                bakedLevels[0] = 0;
                break;
            case 1:
                bakedLevels[1] = 0;
                break;
            case 2:
                bakedLevels[2] = 0;
                break;
            case 3:
                bakedLevels[3] = 0;
                break;
            default:
                break;
        }
    }

    public void AffectLevels(int first, int second, int third, int fourth)
    {
        bakedLevels[0] += first;
        bakedLevels[1] += second;
        bakedLevels[2] += third;
        bakedLevels[3] += fourth;
        UpdateLevels(bakedLevels);
    }

    private void RollValues(bool advantageRoll,float level, int choice)
    {
        Debug.Log(level);
        int employeeRoll = Random.Range(1, 21) + (int)addedNum;
        int customerRoll = Random.Range(1, 21) + (roundsTaken/3);
        if(customerRoll >= 50)
        {
            customerRoll = 50;
        }

        if (advantageRoll)
        {
            employeeRoll += 5;
        }

        if (level >= 1)
        {
            int levelBonus = (int)Mathf.Exp((1 + (.5f * level)));
            employeeRoll += levelBonus;
            Debug.Log(levelBonus + " level bonus");
            
        }

        int adjustedRoll = System.Convert.ToInt32(employeeRoll + Mathf.Pow(1.8f, level));

        if (customerRoll < employeeRoll)
        {
            //AudioManager.instance.PlaySingle(successAudio);

            //reactionPanel.GetComponent<DisplayReaction>().ProcessReaction(choice, 0); panel not active
            responseField.text = "The Employee Rolled " + employeeRoll + " And the Customer Rolled " + customerRoll + ". Employee Wins!";
        }
        if (customerRoll >= employeeRoll)
        {

            //AudioManager.instance.PlaySingle(failAudio);

            //reactionPanel.GetComponent<DisplayReaction>().ProcessReaction(choice, 1); panel not active
            responseField.text = "The Employee Rolled " + employeeRoll + " And the Customer Rolled " + customerRoll + ". Customer Wins!";
        }
        ModifyNumberByItem(0,0);
        int totalRoll = employeeRoll - customerRoll;
        PassScore(totalRoll);
        roundsTaken++;
    }

    private void PassScore(int roll)
    {
        Debug.Log("The roll is" + roll);
        HeadPhoneManager headPhone = FindObjectOfType<HeadPhoneManager>();
        headPhone.GetValue(roll);
    }

    public void UpdateLevels(int[] levels)
    {
        for(int i = 0; i< responseLevelFields.Length;i++)
        {
            LevelTextHolder currentText = responseLevelFields[i].GetComponent<LevelTextHolder>();
            currentText.ReceiveValue(levels[i]);
        }
    }
}
