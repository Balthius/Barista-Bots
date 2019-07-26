using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarShopItem : MonoBehaviour
{

    [SerializeField]
    private GameObject purchaseItem;

    [SerializeField]
    private int currentItemCost;

    private bool boughtStatus;

    public void Awake()
    {
        LoadStatus();
    }

   
    private void SaveStatus()
    {
        string itemStatus = purchaseItem.name + "ShopItem";


        PlayerPrefs.SetString(itemStatus, boughtStatus.ToString());
    }

    private void LoadStatus()
    {
        string itemStatus = purchaseItem.name + "ShopItem";


        string currentStatus = PlayerPrefs.GetString(itemStatus);

        if (currentStatus == "True")
        {
            boughtStatus = true;
        }
        else
        {
            Debug.Log(purchaseItem.name + "Not Bought");
        }
    }

}
