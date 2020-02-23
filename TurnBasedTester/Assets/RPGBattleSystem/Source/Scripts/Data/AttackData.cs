using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class AttackData : ScriptableObject
{
    public new string name;
    public string description;

    public Sprite portrait;
    public Element element;
    public GameObject prefab;

    public AttackType attackType;
    public AttackMoveType moveType;
    public HitTarget hitTarget;

    public int cost;
    public int power;
    public int count;
    public float delayBetweenAttacks;
    public float moveSpeed;

    private string displayDescription;

    public string getDisplayDescription()
    {
        if (!string.IsNullOrEmpty(description))
        {
            displayDescription = description.Replace("{power}", power.ToString())
                                       .Replace("{count}", count.ToString());
        }

        return displayDescription;
    }
}
