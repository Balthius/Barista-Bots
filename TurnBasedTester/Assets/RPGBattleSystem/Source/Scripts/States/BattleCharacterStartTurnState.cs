using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacterStartTurnState : BattleState
{
    public BattleCharacterStartTurnState(ApplicationController p_controller) : base(p_controller)
    {

    }

    public override void enter()
    {
        // select the character that's starting this turn. 
        Entity entity = m_battleController.selectNextEntity();

        m_battleOverlayCanvas = m_viewController.open<BattleOverlayCanvas>(ViewDefinition.BATTLE_OVERLAY_MENU);

        if (entity != null)
        {
            // set the entity to be applied to the UI screen
            m_battleOverlayCanvas.setSelectedEntity(entity.state);

            entity.OnCharacterTurnStart();

            if (entity.team == Team.PLAYER)
            {
                m_stateController.changeState(StateDefinition.BATTLE_WAIT_FOR_PLAYER_ACTION);
            }
            else
            {
                m_stateController.changeState(StateDefinition.BATTLE_WAIT_FOR_AI_ACTION);
            }
        }
    }

    protected BattleOverlayCanvas m_battleOverlayCanvas;
}
