using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The start of the "turn"
/// A turn is the series of "character" turns, it makes up every character
/// on the battlefield.
/// </summary>
public class BattleTurnStartState : BattleState
{
    public BattleTurnStartState(ApplicationController p_controller)
        : base(p_controller)
    {

    }

    public override void enter()
    {
        // we set all characters to an "active" state which means 
        // that they are allowed to take a turn (attack/heal/etc)

        m_battleController.turnStart();

        // do other turn start things?

        // change the state to start the next characters turn
        m_stateController.changeState(StateDefinition.BATTLE_CHARACTER_TURN_START);
    }
}
