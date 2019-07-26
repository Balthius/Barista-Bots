using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStatusController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] customerBots;
	// Use this for initialization
	void Start ()
    {
        CheckBots(1);
    }

    public void RollHour(int time)
    {
        CheckBots(time);
    }

    private void CheckBots(int passedHours)
    {
        var foundObjects = FindObjectsOfType<Mess>();
        foreach(Mess currentMess in foundObjects)
        {
            Destroy(currentMess.gameObject);
        }
      
       
        foreach (GameObject Bots in customerBots)
        {
            CustomerAppearance currentBot = Bots.GetComponent<CustomerAppearance>();
           
            try
            {
                string messStatus = currentBot.robotName + "Mess";

                int messVal = PlayerPrefs.GetInt(messStatus);

                currentBot.CheckCustomer(currentBot.robotName, messVal);
            }
            catch
            {
                currentBot.TempSaveValues(currentBot.robotName);
            }

            if(!currentBot.hasMess)
            {
                currentBot.UpdateSpawn(passedHours);
            }
        }
    }

 
    // Update is called once per frame
    void Update () {
		
	}
}
