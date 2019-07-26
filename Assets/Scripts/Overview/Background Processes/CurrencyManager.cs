using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour

{

    public static int currentCafeCurrency;
    [SerializeField]
    Text currencyText;

    static DateTime savedTime;
    static DateTime newTime;

    public static DateTime SavedTime
    {
        get
        {
            return savedTime;
        }

        set
        {
            savedTime = value;
        }
    }

    public static DateTime NewTime
    {
        get
        {
            return newTime;
        }

        set
        {
            newTime = value;
        }
    }

    

    static void Quit()
    {
        SaveCurrency();
    }

    [RuntimeInitializeOnLoadMethod]
    static void RunOnStart()
    {
        Application.quitting += Quit;
        if(PlayerPrefs.HasKey("SaveTime"))
        {

            LoadTime();
        }
        else
        {
            SavedTime = DateTime.Now;
        }
        
        IdleTime();
    }
    public void Update()
    {
        currencyText.text = "$" + currentCafeCurrency.ToString(); 
    }

    static public void AddCurrency(int score)
    {
        currentCafeCurrency += score;
    }
    static public void RemoveCurrency(int score)
    {
        currentCafeCurrency -= score;
    }

    public void ResetCurrency()
    {
        currentCafeCurrency = 0;
    }

    public void CheckBots()
    {
       

        TimeSpan idleHours = NewTime - SavedTime;
        int checkTime = (int)idleHours.TotalHours;

       Debug.Log(checkTime);
        for(int i = 0; i < checkTime; i++)
        {
            Debug.Log(i);
        }
    }

    static public void SaveCurrency()
    {

        NewTime = DateTime.Now;
        PlayerPrefs.SetInt("Currency", currentCafeCurrency);

        PlayerPrefs.SetString("SaveTime", NewTime.ToBinary().ToString());
    }

    static public void LoadTime()
    {
        currentCafeCurrency = PlayerPrefs.GetInt("Currency");
        long temp = Convert.ToInt64(PlayerPrefs.GetString("SaveTime"));
        SavedTime = DateTime.FromBinary(temp);
     
    }

    static private void IdleTime()
    {
        NewTime = DateTime.Now;
        TimeSpan idleSeconds = NewTime - SavedTime;

        int idleDuration = (int)idleSeconds.TotalSeconds;
    }
}
