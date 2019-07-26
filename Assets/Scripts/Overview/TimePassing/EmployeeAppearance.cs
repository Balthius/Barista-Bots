using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeAppearance : BotAppearance {
    
    public void CheckBotLevel()
    {
        botLevel = RecallSavedValues(robotName);
    }
}
