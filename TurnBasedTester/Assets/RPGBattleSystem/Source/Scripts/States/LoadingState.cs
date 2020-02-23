using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingState : State
{
    public LoadingState(ApplicationController p_controller) : base(p_controller) { }

    public override void enter()
    {
        // this is a great place to put plugin stuff, load in player data, etc.

        m_stateController.changeState(StateDefinition.CHARACTER_SELECT);
    }

}
