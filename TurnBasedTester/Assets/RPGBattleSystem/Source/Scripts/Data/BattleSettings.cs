using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleSettings
{
    public List<EntityData> entities;
    public EnvironmentData environment;

    public BattleSettings()
    {
        entities = new List<EntityData>();
    }
}
