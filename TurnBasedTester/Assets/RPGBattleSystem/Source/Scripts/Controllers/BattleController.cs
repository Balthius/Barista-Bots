using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BattleController : Controller
{
    [SerializeField]
    protected BattleFieldNode[] m_fieldNodes;
    [SerializeField]
    protected BattleSettings m_defaultSettings;
    [SerializeField]
    protected EnvironmentSetting m_environment;
    [SerializeField]
    protected CanvasGroup m_shadowScreen;

    public Entity currentEntity
    {
        get { return m_currentEntity; }
    }

    public Entity currentAttackTarget
    {
        get { return m_currentAttackTarget; }
    }

    public EntityAttackData currentAttackData
    {
        get { return m_currentAttackData; }
    }

    public event Action turnStartAction = delegate { };
    public event Action<Entity, EntityAttackData> selectedAttackTargetAction = delegate { };
    public event Action attackFinishedAction = delegate { };
    public event Action<Entity> entityDeathAction = delegate { };

    public void initBattle(BattleSettings p_battleSettings = null)
    {
        BattleSettings settings = null;

        // use the passed in battle settings first
        if (p_battleSettings != null)
        {
            settings = p_battleSettings;
        }
        // try the user settings as second priority
        else if (m_userSettings != null)
        {
            settings = m_userSettings;
        }
        // finally use the default settings if nothing else, default should only be used for debugging
        else
        {
            settings = m_defaultSettings;
        }

        m_entities = new List<Entity>();

        for (int i = 0; i < m_fieldNodes.Length; i++)
        {
            for (int j = m_fieldNodes[i].transform.childCount - 1; j > -1; j--)
            {
                Destroy(m_fieldNodes[i].transform.GetChild(j).gameObject);
            }
        }

        for (int i = 0; i < settings.entities.Count; i++)
        {
            EntityData entityData = settings.entities[i];
            GameObject entityObj = Instantiate(entityData.prefab);
            Entity entity = entityObj.GetComponent<Entity>();
            BattleFieldNode fieldNode = m_fieldNodes[i];
            entity.initialize(m_controller, entityData, fieldNode.team);
            entity.setFieldNode(fieldNode);
            m_entities.Add(entity);
        }

        m_environment.initialize(settings.environment);
    }

    public void setBattleSettings(BattleSettings p_settings)
    {
        m_userSettings = p_settings;
    }

    public virtual void turnStart()
    {
        for (int i = 0; i < m_entities.Count; i++)
        {
            m_entities[i].OnTurnStart();
        }

        turnStartAction();
    }

    public void registerEntityDeath(Entity p_entity)
    {
        if (m_entities.Contains(p_entity))
        {
            m_entities.Remove(p_entity);
        }

        // subscribe to this to update UI components accordingly
        // or for any other reasons
        entityDeathAction(p_entity);
    }

    public void executePendingAttack()
    {
        if (m_currentEntity == null || m_currentAttackData == null)
        {
            return;
        }

        Entity[] targets = null;

        if (m_currentAttackData.attackData.hitTarget == HitTarget.SINGLE)
        {
            targets = new Entity[] { m_currentAttackTarget };
        }
        else if (m_currentAttackData.attackData.hitTarget == HitTarget.SELF)
        {
            targets = new Entity[] { m_currentEntity };
        }
        else if (m_currentAttackData.attackData.hitTarget == HitTarget.RANDOM_ALL)
        {
            targets = new Entity[m_currentAttackData.attackData.count];

            for (int i = 0; i < targets.Length; i++)
            {
                targets[i] = m_entities[UnityEngine.Random.Range(0, m_entities.Count)];
            }
        }
        else if (m_currentAttackData.attackData.hitTarget == HitTarget.RANDOM_ENEMIES)
        {
            targets = new Entity[m_currentAttackData.attackData.count];

            for (int i = 0; i < targets.Length; i++)
            {
                Entity entity = null;

                while (entity == null || entity.team == m_currentEntity.team)
                {
                    entity = m_entities[UnityEngine.Random.Range(0, m_entities.Count)];
                }

                targets[i] = entity;
            }
        }
        else if (m_currentAttackData.attackData.hitTarget == HitTarget.RANDOM_FRIENDLY)
        {
            targets = new Entity[m_currentAttackData.attackData.count];

            for (int i = 0; i < targets.Length; i++)
            {
                Entity entity = null;

                while (entity == null || entity.team != m_currentEntity.team)
                {
                    entity = m_entities[UnityEngine.Random.Range(0, m_entities.Count)];
                }

                targets[i] = entity;
            }
        }

        m_currentEntity.attackFinishedAction += onAttackFinished;
        m_currentEntity.startAttack(m_currentAttackData, targets);
    }

    protected virtual void onAttackFinished()
    {
        m_currentEntity.attackFinishedAction -= onAttackFinished;
        attackFinishedAction();
    }

    public void highlightAttackTargets()
    {
        if (m_currentEntity == null || m_currentAttackData == null)
            return;

        // add other hit targets and highlight them appropriately
        if (m_currentAttackData.attackData.hitTarget == HitTarget.SINGLE)
        {
            for (int i = 0; i < m_entities.Count; i++)
            {
                if (m_currentEntity.team != m_entities[i].team)
                {
                    m_entities[i].highlightEntity(true, Color.red);
                    m_entities[i].setInteractable(true);
                    m_entities[i].setSortOrder(200);
                    m_entities[i].selectedAction += onSelectedAttackTarget;
                }
                else
                {
                    m_entities[i].highlightEntity(false, Color.white);
                    m_entities[i].setInteractable(false);
                    m_entities[i].resetSortOrder();
                }
            }

            m_shadowScreen.DOFade(1f, 0.5f);
        }
    }

    public void cancelHighlightAttack()
    {
        m_shadowScreen.DOFade(0f, 0.5f);

        for (int i = 0; i < m_entities.Count; i++)
        {
            m_entities[i].highlightEntity(false, Color.white);
            m_entities[i].setInteractable(false);
            m_entities[i].resetSortOrder();
            m_entities[i].selectedAction -= onSelectedAttackTarget;
        }
    }

    public void onSelectedAttackTarget(Entity p_target)
    {
        cancelHighlightAttack();

        m_currentAttackTarget = p_target;

        selectedAttackTargetAction(m_currentAttackTarget, m_currentAttackData);
    }

    public bool isAnyEntityActive()
    {
        bool active = false;

        for (int i = 0; i < m_entities.Count; i++)
        {
            active = m_entities[i].active && !m_entities[i].dead;

            if (active)
            {
                break;
            }
        }

        return active;
    }

    /// <summary>
    /// Returns if the battle is finished by checking if 1 or both teams are dead.
    /// </summary>
    /// <returns><c>true</c>, if battle finished was ised, <c>false</c> otherwise.</returns>
    public bool isBattleFinished()
    {
        // do win conditoins and lose conditions here to finish a battle.

        bool teamPlayerAlive = false;
        bool teamEnemyAlive = false;

        for (int i = 0; i < m_entities.Count; i++)
        {
            if (m_entities[i].team == Team.PLAYER)
            {
                teamPlayerAlive = teamPlayerAlive || !m_entities[i].dead;
            }
            if (m_entities[i].team == Team.ENEMY)
            {
                teamEnemyAlive = teamEnemyAlive || !m_entities[i].dead;
            }
        }

        return !(teamPlayerAlive && teamEnemyAlive);
    }

    /// <summary>
    /// Gets the winning team.
    /// Tie goes to the Player in this scenario
    /// Expects "isBattleFinished" to be called first otherwise it gives back a false win of player.
    /// </summary>
    /// <returns>The winning team.</returns>
    public Team getWinningTeam()
    {
        Team winningTeam = Team.PLAYER;
        bool teamPlayerAlive = false;
        bool teamEnemyAlive = false;

        for (int i = 0; i < m_entities.Count; i++)
        {
            if (m_entities[i].team == Team.PLAYER)
            {
                teamPlayerAlive = teamPlayerAlive || !m_entities[i].dead;
            }
            if (m_entities[i].team == Team.ENEMY)
            {
                teamEnemyAlive = teamEnemyAlive || !m_entities[i].dead;
            }
        }

        if (teamPlayerAlive)
        {
            winningTeam = Team.PLAYER;
        }

        if (teamEnemyAlive)
        {
            winningTeam = Team.ENEMY;
        }

        return winningTeam;

    }

    public Entity selectNextEntity()
    {
        Entity next = null;

        for (int i = 0; i < m_entities.Count; i++)
        {
            Entity entity = m_entities[i];

            if (!entity.active || entity.dead)
            {
                continue;
            }

            if (next == null || next.state.currentSpeed < entity.state.currentSpeed)
            {
                next = entity;
            }
        }

        if (m_currentEntity != null)
        {
            m_currentEntity.highlightEntity(false, Color.white);
        }

        m_currentEntity = next;

        if (m_currentEntity != null)
        {
            m_currentEntity.highlightEntity(true, Color.white);
        }

        m_currentAttackData = null;
        m_currentAttackTarget = null;

        return m_currentEntity;
    }

    public void setCurrentAttackData(AttackData p_data)
    {
        m_currentAttackData = m_currentEntity.state.data.attacks
                                             .Find(x => x.attackData.name == p_data.name);
    }

    public List<Entity> getEntitiesByTeam(Team p_team)
    {
        return m_entities.FindAll(x => x.team == p_team);
    }


    protected BattleSettings m_userSettings;
    protected List<Entity> m_entities;
    protected Entity m_currentEntity;
    protected Entity m_currentAttackTarget;
    protected EntityAttackData m_currentAttackData;
}
