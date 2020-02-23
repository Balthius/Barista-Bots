using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu]
public class EntityData : ScriptableObject
{
    public new string name;
    public string description;
    public Sprite portrait;
    public Element element;
    public EntityStatsData stats;
    public GameObject prefab;
    public List<EntityAttackData> attacks;
}
