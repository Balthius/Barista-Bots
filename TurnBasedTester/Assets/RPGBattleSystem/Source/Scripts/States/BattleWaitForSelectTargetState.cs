using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWaitForSelectTargetState : BattleState
{
    public BattleWaitForSelectTargetState(ApplicationController p_controller)
        : base(p_controller)
    {

    }

    public override void enter()
    {
        m_battleOverlayCanvas = m_viewController.open<BattleOverlayCanvas>(ViewDefinition.BATTLE_OVERLAY_MENU);
        m_battleOverlayCanvas.setConsoleLabel(string.Format("Select a <color=#FF3F44FF>target</color>."));
        m_battleOverlayCanvas.setCancelButtonActive(true);
        m_battleOverlayCanvas.cancelButtonAction += OnCancelSelectTarget;
        m_battleController.highlightAttackTargets();
        m_battleController.selectedAttackTargetAction += onSelectedAttackTarget;
    }

    public override void exit()
    {
        m_battleOverlayCanvas.setCancelButtonActive(false);
        m_battleController.selectedAttackTargetAction -= onSelectedAttackTarget;
        m_battleOverlayCanvas.cancelButtonAction -= OnCancelSelectTarget;
    }

    public virtual void onSelectedAttackTarget(Entity p_target, EntityAttackData p_attackData)
    {
        m_stateController.changeState(StateDefinition.BATTLE_EXECUTE_ACTION);
    }

    private void OnCancelSelectTarget()
    {
        m_battleController.cancelHighlightAttack();
        m_stateController.changeState(StateDefinition.BATTLE_WAIT_FOR_PLAYER_ACTION);
    }

    protected BattleOverlayCanvas m_battleOverlayCanvas;
}
