using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAppearance :BotAppearance
{

    private bool botActive = false;

    public bool hasMess = false;
    [SerializeField]
    private GameObject botMess;

    private int botTimeStayed; // tracks outside of function for math purposes
   

    public void CheckCustomer(string robotName, int messVal) // Checks stored values from controller
    {
        if (messVal == 1) // if 1 create mess, mess will be doubly sure and set mess to 1
        {
                CreateMess();
                this.gameObject.SetActive(false);
        }
        else
        {
            hasMess = false;
        }
    }


    public void UpdateSpawn(int hours)
    {
        if (!hasMess && hours >= 1)// if no mess, and an hour or more has passed roll number of hours, decide if bot can be activated or not, mess created or not 
        {
            for (int i = 1; i < hours; i++)
            {
                int roll = SpawnChance(i);
                int bar = Random.Range(0, 101);
                
                if (bar <= roll && !botActive)
                {
                    this.gameObject.SetActive(true);
                    botActive = true;
                    botTimeStayed = 1;
                }
                if (bar <= roll && botActive)
                {
                    botTimeStayed++;
                }
                if (bar >= roll && !botActive)
                {
                }
                if (bar >= roll && botActive)
                {
                    PayoutVisit(botTimeStayed, botLevel);
                    SaveValues(robotName, botLevel, botTimeStayed);
                    return;
                }
                else
                {
                }
            }
        }
        else
        {//If there is a mess set false
            this.gameObject.SetActive(false);
        }
    }

    private void PayoutVisit(int time, int level)
    {
        float multiplier = 1.6f + (.05f * level);
        int pay = (int)(5 + (Mathf.Pow(multiplier, time)));
        botTimeStayed = 0;
        CreateMess();
        CurrencyManager.currentCafeCurrency += pay;
        this.gameObject.SetActive(false);
    }

    private int SpawnChance(int i)
    {
        int levelValue = AdjustActiveValues(botLevel);
        int gameValue = WorldManager.minigameLevels[0];
        int timeValue = i;
        int total = levelValue + gameValue - timeValue;
        return total;
    }
    
    private int AdjustActiveValues(int level)
    {
        switch (level)
        {
            case 1:
                return 5;
            case 2:
                return 10;
            case 3:
                return 15;
            case 4:
                return 20;
            default:
                return 25;
        }
    }

    private void CreateMess()
    {
        Vector3 adjustPosition = new Vector3(0, 0, 1);
        Vector3 newPosition = transform.position - adjustPosition;
     
            GameObject newMess = Instantiate(botMess, newPosition, Quaternion.identity);
            newMess.GetComponent<Mess>().SaveStatus(robotName);
            hasMess = true;
    }
}
