using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour {

    [SerializeField]
    private int currentItemCost;

    private bool boughtStatus;

    public void Awake()
    {
        LoadStatus();
    }
    public void BuyItem()
    {
        UpgradePanelManager upgrade = FindObjectOfType<UpgradePanelManager>();
        string name = this.gameObject.name;
        upgrade.UpgradeItem(name, currentItemCost, boughtStatus, this.gameObject);
    }

    private void SaveStatus()
    {
        string itemStatus = this.name + "ShopItem";


        PlayerPrefs.SetString(itemStatus, boughtStatus.ToString());
    }

    private void LoadStatus()
    {
        string itemStatus = this.name + "ShopItem";


        string currentStatus = PlayerPrefs.GetString(itemStatus);

        if (currentStatus == "True")
        {
            boughtStatus = true;
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {

        }
    }

    public void PurchaseItem()
    {
        boughtStatus = true;
        CurrencyManager.currentCafeCurrency -= currentItemCost;
    }

}