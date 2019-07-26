using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    [SerializeField]
    GameObject upgradePanel;
    public static bool panelOpen = false;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                if (hit.collider != null)
                {
                    CloseWindow hitReciever = hit.collider.gameObject.GetComponent<CloseWindow>();

                    if (hitReciever != null)
                    {
                        Debug.Log("Hit recieved 1");
                        hitReciever.CloseCard();
                    }
                    BotPanelControl hitReciever2 = hit.collider.gameObject.GetComponent<BotPanelControl>();
                    if (hitReciever2 != null && !panelOpen)
                    {

                        Debug.Log("Hit recieved 2");
                        hitReciever2.EnableCard();
                    }

                    SwitchPage hitReciever3 = hit.collider.gameObject.GetComponent<SwitchPage>();
                    if (hitReciever3 != null)
                    {

                        Debug.Log("Hit recieved 3");
                        hitReciever3.TriggerSwitch();
                    }

                    RunGame hitReciever4 = hit.collider.gameObject.GetComponent<RunGame>();
                    if (hitReciever4 != null)
                    {

                        Debug.Log("Hit recieved 4");
                        hitReciever4.LoadLevel();


                    }
                    GoToLocation hitReciever5 = hit.collider.gameObject.GetComponent<GoToLocation>();
                    if (hitReciever5 != null && !panelOpen)
                    {

                        Debug.Log("Hit recieved 5");
                        hitReciever5.GoToObject();
                    }

                    ShopItem hitReciever6 = hit.collider.gameObject.GetComponent<ShopItem>();
                    if(hitReciever6 != null && !panelOpen)
                    {
                        upgradePanel.SetActive(true);
                        hitReciever6.BuyItem();
                        panelOpen = true;
                    }

                    Mess hitReceiver7 = hit.collider.gameObject.GetComponent<Mess>();
                    if(hitReceiver7 != null)
                    {
                        hitReceiver7.ClearMess();
                    }

                    //Ingredient hitReceiver8 = hit.collider.gameObject.GetComponent<Ingredient>();
                    //if (hitReceiver8 != null)
                    //{
                    //    hitReceiver8.DestroyOnClick();removed

                }
            }
        }
    }
}
