using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{

    public GameObject panelOne;
    public GameObject panelTwo;

    private bool panelState = false;
    private void Start()
    {
        DisableCard();
    }

    public void DisableCard()
    {
        this.gameObject.SetActive(false);
    }

    public void EnableCard()
    {
        this.gameObject.SetActive(true);
    }
    public void OnActivate()
    {
        if(!panelState)
        {
            panelOne.SetActive(true);
            panelTwo.SetActive(false);
            panelState = !panelState;
        }
        else
        {
            panelOne.SetActive(false);
            panelTwo.SetActive(true);
            panelState = !panelState;
        }
    }
   
}
