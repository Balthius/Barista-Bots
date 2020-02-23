using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The base class for all battle states
/// Place variables that you will most likely need for battles in this base class for easy reference
/// </summary>
public class BattleState : State
{
    public BattleState(ApplicationController p_controller) : base(p_controller)
    {
        m_battleController = m_controller.getController<BattleController>();
    }

    public BattleController m_battleController;
}
