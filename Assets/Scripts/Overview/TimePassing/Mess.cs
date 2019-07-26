using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mess : MonoBehaviour
{
    private string messStatus; //Stored while active

    public void SaveStatus(string bot)// saves bot this was created from, sets playerprefs value to mess
    {
        messStatus = bot + "Mess";
        CreateMess();
    }
    public void ClearMess() //clears current mess (On click, event manager)
    { 
        PlayerPrefs.SetInt(messStatus, 0);
        Destroy(gameObject);
    }
    
    public void CreateMess() // From SaveStatus
    {
            PlayerPrefs.SetInt(messStatus, 1);
    }
    
}
