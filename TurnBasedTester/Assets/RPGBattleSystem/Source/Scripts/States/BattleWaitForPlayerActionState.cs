using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWaitForPlayerActionState : BattleState
{
    public BattleWaitForPlayerActionState(ApplicationController p_controller) : base(p_controller)
    {

    }

    public override void enter()
    {
        m_battleOverlayCanvas = m_viewController.open<BattleOverlayCanvas>(ViewDefinition.BATTLE_OVERLAY_MENU);
        m_battleOverlayCanvas.turnConsoleOff();
        string moderatorText = string.Format("Choose an attack for <color=#FF3F44FF>{0}</color>.", m_battleController.currentEntity.state.data.name);
        m_battleOverlayCanvas.setModeratorLabel(moderatorText);

        m_battleOverlayCanvas.attackButtonPressed += onAttackButtonPressed;
    }

    protected virtual void onAttackButtonPressed(AttackData p_data)
    {
        m_battleOverlayCanvas.attackButtonPressed -= onAttackButtonPressed;
        m_battleController.setCurrentAttackData(p_data);

        if (p_data.hitTarget == HitTarget.SINGLE)
        {
            m_stateController.changeState(StateDefinition.BATTLE_WAIT_SELECT_TARGET);
        }
        else
        {
            m_stateController.changeState(StateDefinition.BATTLE_EXECUTE_ACTION);
        }

    }

    protected BattleOverlayCanvas m_battleOverlayCanvas;
}
