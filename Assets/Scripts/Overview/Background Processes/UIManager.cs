using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public void ResetCurrency()
    {
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        currencyManager.ResetCurrency();
        Debug.Log("hit Detected");
    }
    public void CheckBots()
    {
        CurrencyManager currencyManager = FindObjectOfType<CurrencyManager>();
        currencyManager.CheckBots();
    }
}
