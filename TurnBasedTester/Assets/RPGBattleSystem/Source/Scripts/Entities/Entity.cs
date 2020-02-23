
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected UIEnergyBar m_healthBar;
    [SerializeField]
    protected SpriteRenderer m_shadowRenderer;

    public List<EntityAttackSettings> attackMap;

    public bool active
    {
        get { return m_active; }
    }

    public Team team
    {
        get { return m_team; }
    }

    public EntityState state
    {
        get { return m_state; }
    }

    public bool dead
    {
        get { return m_dead; }
    }

    public Vector2 position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public event Action<Entity> selectedAction = delegate { };
    public event Action attackFinishedAction = delegate { };
    public event Action<int> damaged = delegate { };

    public virtual void initialize(ApplicationController p_controller, EntityData p_data, Team p_team)
    {
        m_state = new EntityState(p_data);
        m_team = p_team;
        m_active = true;
        m_outline = GetComponentInChildren<SpriteOutline>();
        m_outline.outline = false;
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        m_animator = GetComponentInChildren<Animator>();
        m_animator.Play("Idle");
        m_selectButton = GetComponentInChildren<Button>();
        m_selectButton.onClick.AddListener(OnSelected);
        m_soundTarget = Camera.main.transform.position;
        m_rendererOffset = m_renderer.transform.localPosition;

        m_healthBar.SetSliderValues(0, m_state.data.stats.health, m_state.currentHealth);

        m_controller = p_controller;
        m_battleController = m_controller.getController<BattleController>();
    }

    /// <summary>
    /// Starts the attack with targets and data of what attack to use
    /// This does not fire any effects or projectiles
    /// Waits for the "attackCallback" method to be fired from the animator
    /// This gives total control to the animator for timing reasons.
    /// </summary>
    /// <param name="p_data">P data.</param>
    /// <param name="p_targets">P targets.</param>
    public virtual void startAttack(EntityAttackData p_data, Entity[] p_targets)
    {
        m_activeAttack = p_data;
        m_attackTargets = p_targets;



        EntityAttackSettings attackSettings = attackMap.Find(x => x.animation == m_activeAttack.animation);

        setSortOrder(100);

        if (attackSettings.attackStartSound != null)
        {
            AudioSource.PlayClipAtPoint(attackSettings.attackStartSound, m_soundTarget);
        }

        if (m_attackTargets.Length > 0)
        {
            // do I move up close or stay in my position?
            if (m_activeAttack.attackData.attackType == AttackType.MELEE)
            {
                if (m_attackTargets.Length > 1)
                {
                    // we need to move to the middle of the targets
                }
                else
                {
                    Transform attackLocation = attackSettings.attackSpawnLocation;

                    Vector2 offset = transform.position - attackLocation.position;

                    m_renderer.transform.DOMove(m_attackTargets[0].position + offset, 0.5f)
                              .SetEase(Ease.InBack)
                              .OnComplete(() => m_animator.Play(m_activeAttack.animation));
                }
            }
            else
            {
                // stay here (animator will handle timing for attack!)
                m_animator.Play(m_activeAttack.animation);
            }
        }
        else
        {
            Debug.LogErrorFormat("No attack targets found!");
        }
    }

    /// <summary>
    /// Uses a coroutine to spawn various effects and objects for attacks.
    /// This is called from the animator to be synced with animations to 
    /// provide better looking attacks with timings.
    /// </summary>
    public virtual void OnAttack()
    {
        StartCoroutine(attack());
    }

    /// <summary>
    /// The attack coroutine, allows for delayed attacks when attacking 
    /// multiple times and for general delays
    /// </summary>
    /// <returns>The attack.</returns>
    protected virtual IEnumerator attack()
    {
        m_state.modifyMana(-m_activeAttack.attackData.cost);

        float delayBetweenAttacks = m_activeAttack.attackData.delayBetweenAttacks;

        m_pendingAttackWaitCount = m_attackTargets.Length;

        if (m_pendingAttackWaitCount <= 0)
        {
            Debug.LogError("Pending attack targets is zero!");
        }

        for (int i = 0; i < m_attackTargets.Length; i++)
        {
            GameObject attackObj = Instantiate(m_activeAttack.attackData.prefab);
            Attack attackScript = attackObj.GetComponent<Attack>();
            attackScript.initialize(m_attackTargets[i], m_activeAttack.attackData);
            attackScript.explodeAction += OnAttackExplode;
            Vector2 spawnPoint = m_attackTargets[i].transform.position;
            attackScript.flipX(transform.position.x < spawnPoint.x);

            if (m_activeAttack.attackData.moveType == AttackMoveType.PROJECTILE)
            {
                float speed = m_activeAttack.attackData.moveSpeed;
                Transform attackLocation = attackMap.Find(x => x.animation == m_activeAttack.animation).attackSpawnLocation;
                attackObj.transform.position = attackLocation.position;

                attackScript.move(spawnPoint, speed);
            }
            else
            {
                attackObj.transform.position = spawnPoint;
                attackScript.explode();
            }


            yield return new WaitForSeconds(delayBetweenAttacks);
        }

        // we need to move a melee attacker back to it's original position
        if (m_activeAttack.attackData.attackType == AttackType.MELEE)
        {
            // lets give a nice added pause so the attacker doesn't move back so fast
            yield return new WaitForSeconds(1f);

            m_renderer.transform.DOLocalMove(m_rendererOffset, 0.5f)
                      .SetEase(Ease.OutBack);

            // also give a pause when they move back
            yield return new WaitForSeconds(1f);

            resetSortOrder();
        }
    }

    /// <summary>
    /// The callback for when an attack hit's it's target and explodes,
    /// This is used for timing when the attack hits an entity for animation
    /// reasons.
    /// </summary>
    /// <param name="p_target">P target.</param>
    /// <param name="p_data">P data.</param>
    protected virtual void OnAttackExplode(Entity p_target, AttackData p_data)
    {
        p_target.damage(p_data);
        m_pendingAttackWaitCount--;

        if (m_pendingAttackWaitCount <= 0)
        {
            attackFinishedAction();
        }
    }

    /// <summary>
    /// Deals damage to an entity and calls its damage or death animation
    /// </summary>
    /// <returns>The damage.</returns>
    /// <param name="p_data">P data.</param>
    public virtual void damage(AttackData p_data)
    {
        int damageAmount = -p_data.power;
        m_state.modifyHealth(damageAmount);
        m_healthBar.FillValue(damageAmount);

        damaged(damageAmount);

        if (m_state.currentHealth <= 0)
        {
            death();
        }
        else
        {

            m_animator.Play("Damaged");
        }
    }

    /// <summary>
    /// Kills this entity, dont forget to place a callback "deathCallback"
    /// in it's animator
    /// </summary>
    protected virtual void death()
    {
        m_dead = true;
        m_animator.Play("Death");

        StartCoroutine(waitForDeath());
    }

    protected IEnumerator waitForDeath()
    {
        while (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            yield return null;
        }

        float deathTime = m_animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(deathTime);

        OnDeath();
    }

    /// <summary>
    /// Highlights the entity using the sprite outline shader,
    /// you can also provide a color 
    /// </summary>
    /// <param name="p_highlight">If set to <c>true</c> p highlight.</param>
    /// <param name="p_color">P color.</param>
    public virtual void highlightEntity(bool p_highlight, Color p_color)
    {
        m_outline.color = p_color;
        m_outline.outline = p_highlight;
    }

    /// <summary>
    /// Sets the field node for the entity,
    /// Field nodes are basically the "spot" where the entity lives.
    /// </summary>
    /// <param name="p_node">P node.</param>
    public virtual void setFieldNode(BattleFieldNode p_node)
    {
        m_fieldNode = p_node;
        transform.SetParent(p_node.transform);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        setSortOrder(p_node.sortOrder);
    }

    /// <summary>
    /// Sets the sort order for the entity and it's shadow
    /// </summary>
    /// <param name="p_sortOrder">P sort order.</param>
    public virtual void setSortOrder(int p_sortOrder)
    {
        m_renderer.sortingOrder = p_sortOrder;
        m_shadowRenderer.sortingOrder = p_sortOrder - 1;
    }

    /// <summary>
    /// Resets the sorting order based on this entities field node.
    /// </summary>
    public virtual void resetSortOrder()
    {
        setSortOrder(m_fieldNode.sortOrder);
    }

    /// <summary>
    /// Sets the entity interactable for touching
    /// Use the "selectedAction" callback in order to know when the user has touched this entity
    /// </summary>
    /// <param name="p_interactable">If set to <c>true</c> p interactable.</param>
    public virtual void setInteractable(bool p_interactable)
    {
        m_selectButton.interactable = p_interactable;
    }

    /// <summary>
    /// The callback for the select button.
    /// </summary>
    protected virtual void OnSelected()
    {
        selectedAction(this);
    }

    /// <summary>
    /// Called from the animator to sync up timing with death
    /// </summary>
    public virtual void OnDeath()
    {
        gameObject.SetActive(false);
        m_battleController.registerEntityDeath(this);
    }

    /// <summary>
    /// When the battle turn is started, not to confuse this
    /// with the character turn start callback where this characters turn is
    /// started.
    /// </summary>
    public virtual void OnTurnStart()
    {
        if (!m_dead)
        {
            m_active = true;
        }
    }

    /// <summary>
    /// When the characters turn is started, this happens with THIS characters 
    /// turn is actually started, not to confuse the OnTurnStart callback 
    /// where the entire turn has started.
    /// </summary>
    public virtual void OnCharacterTurnStart()
    {

    }

    /// <summary>
    /// When the characters turn has ended, this happens with THIS characters 
    /// turn is actually ends, not to confuse the OnTurnEnd callback 
    /// where the entire turn has ended.
    /// </summary>
    public virtual void OnCharacterTurnEnd()
    {
        m_active = false;
    }

    /// <summary>
    /// When the battle turn has ended, do not confuse this
    /// with the OnCharacterTurnStart callback. This is only called when the 
    /// entire battle turn is over (all characters have had turns).
    /// </summary>
    public virtual void OnTurnEnd()
    {

    }

    protected bool m_active;
    protected bool m_dead;

    protected Team m_team;
    protected EntityState m_state;
    protected EntityAttackData m_activeAttack;
    protected Entity[] m_attackTargets;
    protected Animator m_animator;
    protected SpriteRenderer m_renderer;
    protected SpriteOutline m_outline;
    protected Button m_selectButton;
    protected BattleFieldNode m_fieldNode;
    protected Vector3 m_soundTarget;
    protected Vector2 m_rendererOffset;

    protected int m_pendingAttackWaitCount;

    protected ApplicationController m_controller;
    protected BattleController m_battleController;
}
