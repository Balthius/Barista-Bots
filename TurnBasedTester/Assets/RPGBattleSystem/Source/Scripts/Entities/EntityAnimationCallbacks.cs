using UnityEngine;

public class EntityAnimationCallbacks : MonoBehaviour
{
    private Entity m_entity;

    public void Awake()
    {
        m_entity = GetComponentInParent<Entity>();
    }

    public void attackCallback()
    {
        m_entity.OnAttack();
    }
}
