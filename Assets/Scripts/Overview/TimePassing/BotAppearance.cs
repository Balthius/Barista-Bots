using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAppearance : MonoBehaviour
{
    public int botLevel;
    [SerializeField]
    public string robotName;

    protected int RecallSavedValues(string RobotName)
    {
        string savedLevel = robotName + "Level";
        return PlayerPrefs.GetInt(savedLevel);
    }

    protected void SaveValues(string RobotName, int level, int time)
    {
        string savedLevel = robotName + "Level";
        string messStatus = robotName + "Mess";
        string timeStatus = robotName + "TimeStayed";

        PlayerPrefs.SetInt(savedLevel, level);
        PlayerPrefs.SetInt(messStatus, 1);
        PlayerPrefs.SetInt(timeStatus, time);
    }
    
    public void TempSaveValues(string name)
    {
        SaveValues(name, 1, 1);
    }

}
