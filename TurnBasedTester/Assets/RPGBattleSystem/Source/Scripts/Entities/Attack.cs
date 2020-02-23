using UnityEngine;
using DG.Tweening;
using System;

public class Attack : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    protected GameObject m_hitEffect;
    [SerializeField]
    protected Animator m_animator;
    [SerializeField]
    protected bool m_reverseFlipDirection;
    [Header("Audio")]
    [SerializeField]
    protected AudioClip m_spawnSound;
    [SerializeField]
    protected AudioClip m_hitSound;


    public event Action<Entity, AttackData> explodeAction = delegate { };

    public virtual void initialize(Entity p_target, AttackData p_data)
    {
        m_target = p_target;
        m_data = p_data;
        m_soundTarget = Camera.main.transform.position;

        if (m_spawnSound != null)
        {
            playSound(m_spawnSound);
        }

    }

    public virtual void move(Vector2 p_position, float p_speed)
    {
        transform.DOMove(p_position, p_speed).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(explode);
    }

    public virtual void explode()
    {
        onExplode();

        if (m_hitEffect == null && m_animator != null)
        {
            m_animator.Play("Explode");
            Destroy(gameObject, m_animator.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (m_hitEffect != null)
        {
            GameObject effect = Instantiate(m_hitEffect);
            effect.transform.position = transform.position;

            destroy();
        }
    }

    protected virtual void onExplode()
    {
        explodeAction(m_target, m_data);

        if (m_hitSound != null)
        {
            playSound(m_hitSound);
        }
    }

    public virtual void flipX(bool p_flip)
    {
        if (p_flip && !m_reverseFlipDirection)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (!p_flip && m_reverseFlipDirection)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public virtual void playSound(AudioClip p_clip)
    {
        AudioSource.PlayClipAtPoint(p_clip, m_soundTarget, 1f);
    }

    public virtual void destroy()
    {
        Destroy(gameObject);
    }

    protected Entity m_target;
    protected AttackData m_data;
    protected Vector3 m_soundTarget;
}
