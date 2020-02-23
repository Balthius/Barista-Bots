using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : BattleState
{
    public const float BATTLE_START_DELAY = 2F;

    public BattleStartState(ApplicationController p_controller) : base(p_controller)
    {

    }

    public override void enter()
    {
        m_timer = 0;
        m_battleController.initBattle();
        m_battleOverlayCanvas = m_viewController.open<BattleOverlayCanvas>(ViewDefinition.BATTLE_OVERLAY_MENU);
    }

    public override void update(float p_delta)
    {
        m_timer += p_delta;

        if (m_timer >= BATTLE_START_DELAY)
        {
            m_stateController.changeState(StateDefinition.BATTLE_TURN_START);
        }
    }

    protected float m_timer;
    protected BattleOverlayCanvas m_battleOverlayCanvas;
}