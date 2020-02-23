using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleExecuteAttackState : BattleState
{
    public BattleExecuteAttackState(ApplicationController p_controller)
        : base(p_controller)
    {

    }

    public override void enter()
    {
        m_battleOverlayCanvas = m_viewController.open<BattleOverlayCanvas>(ViewDefinition.BATTLE_OVERLAY_MENU);
        string consoleOutput = string.Format("<color=#FFEB4CFF>{0}</color> used <color=#FF3F44FF>{1}</color>",
                                          m_battleController.currentEntity.state.data.name,
                                          m_battleController.currentAttackData.attackData.name);

        m_battleOverlayCanvas.useMana(m_battleController.currentAttackData.attackData.cost);
        m_battleOverlayCanvas.setConsoleLabel(consoleOutput);
        m_battleController.executePendingAttack();
        m_battleController.attackFinishedAction += onAttackFinish;
    }

    private void onAttackFinish()
    {
        m_battleController.attackFinishedAction -= onAttackFinish;
        m_stateController.changeState(StateDefinition.BATTLE_CHARACTER_TURN_END);
    }

    protected BattleOverlayCanvas m_battleOverlayCanvas;
}
