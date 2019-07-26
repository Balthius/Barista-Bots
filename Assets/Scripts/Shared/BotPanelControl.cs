using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPanelControl : MonoBehaviour {
    public GameObject botCard;
	public void EnableCard()
    {
        botCard.GetComponent<CardControl>().EnableCard();
    }
}
