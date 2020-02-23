using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWaitForAIActionState : BattleState
{
    public const float ARTIFICAL_THINK_TIME = 0.5F;

    public BattleWaitForAIActionState(ApplicationController p_controller)
        : base(p_controller)
    {

    }

    public override void enter()
    {
        m_timeToAction = 0;
        m_battleOverlayCanvas = m_viewController.open<BattleOverlayCanvas>(ViewDefinition.BATTLE_OVERLAY_MENU);
    }

    public override void update(float p_delta)
    {
        m_timeToAction += p_delta;

        if (m_timeToAction >= ARTIFICAL_THINK_TIME)
        {
            doAIAction();
        }
    }

    private void doAIAction()
    {
        // AI selects a random action and random target (if applicable) 
        // add more advanced AI logic inside this class


        // gets the currently selected entity
        Entity entity = m_battleController.currentEntity;
        // selects a random attack from the attack list
        EntityAttackData randomAttack = entity.state.data.attacks[Random.Range(0, entity.state.data.attacks.Count)];
        // set the attack data to the battle controller to be used in the execute action state
        m_battleController.setCurrentAttackData(randomAttack.attackData);

        // if this attack hits only 1 target, choose it randomly
        // set up anything that is required for player touch within the AI action state, as the AI
        // will need to select it themselves.
        if (randomAttack.attackData.hitTarget == HitTarget.SINGLE)
        {
            List<Entity> playerEntities = m_battleController.getEntitiesByTeam(Team.PLAYER);
            m_battleController.onSelectedAttackTarget(playerEntities[Random.Range(0, playerEntities.Count)]);
        }

        m_stateController.changeState(StateDefinition.BATTLE_EXECUTE_ACTION);
    }

    private BattleOverlayCanvas m_battleOverlayCanvas;
    private float m_timeToAction;
}