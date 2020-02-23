
public class BattleTurnEndState : BattleState
{
    public BattleTurnEndState(ApplicationController p_controller)
            : base(p_controller)
    {

    }

    public override void enter()
    {
        // do end turn stuff here!

        m_stateController.changeState(StateDefinition.BATTLE_TURN_START);
    }
}

