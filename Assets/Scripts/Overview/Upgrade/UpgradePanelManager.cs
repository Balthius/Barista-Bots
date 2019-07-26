using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelManager : MonoBehaviour
{
    public Button buyButton;
    public Text textBox;

    private GameObject currentSelectedItem;

    [SerializeField] private AudioClip successAudio;
    [SerializeField] private AudioClip failAudio;

    public void Awake()
    {
        ClosePanel();
    }

    public void ClosePanel()
    {
        this.gameObject.SetActive(false);
        AudioManager.instance.PlaySingle(successAudio);
        EventManager.panelOpen = false;
    }

    public void UpgradeItem(string itemName, int cost, bool maxUpgrade, GameObject currentItem)
    {
        if (!maxUpgrade)
        {
            if (cost <= CurrencyManager.currentCafeCurrency)
            {
                textBox.text = "You have " + CurrencyManager.currentCafeCurrency +
                                                            " to spend and " + itemName + " costs " + cost +
                                                            ", Proceed with purchase?";
                buyButton.gameObject.SetActive(true);
                currentSelectedItem = currentItem;

            }
            else
            {
                textBox.text = "You have " + CurrencyManager.currentCafeCurrency +
                                                           " to spend and " + itemName + " costs " + cost +
                                                           " unfortunately that isn't enough";
                Debug.Log("You're poor");
                buyButton.gameObject.SetActive(false);
            }
        }
        else
        {
            textBox.text = "This item has already been upgraded";
            Debug.Log("This has been upgraded");
            buyButton.gameObject.SetActive(false);
        }
    }

    public void BuyItem()
    {
        ShopItem item = currentSelectedItem.GetComponent<ShopItem>();
        AudioManager.instance.PlaySingle(successAudio);
        item.PurchaseItem();
        ClosePanel();
    }
}
