public class BattleCharacterEndTurnState : BattleState
{
    public const float END_TURN_DELAY = 2F;

    public BattleCharacterEndTurnState(ApplicationController p_controller)
        : base(p_controller)
    {
    }

    public override void enter()
    {
        // TODO: do attack finished stuff.
        // poison damage
        // 

        m_endTurnTimer = 0;
    }

    public override void update(float p_delta)
    {
        m_endTurnTimer += p_delta;

        if (m_endTurnTimer >= END_TURN_DELAY)
        {
            if (m_battleController.currentEntity != null)
            {
                m_battleController.currentEntity.OnCharacterTurnEnd();
            }

            bool isBattleFinished = m_battleController.isBattleFinished();

            if (isBattleFinished)
            {
                Team winningTeam = m_battleController.getWinningTeam();

                if (winningTeam == Team.PLAYER)
                {
                    m_stateController.changeState(StateDefinition.BATTLE_WON);
                }
                else
                {
                    m_stateController.changeState(StateDefinition.BATTLE_LOSE);
                }
            }
            else
            {
                bool isEntityActive = m_battleController.isAnyEntityActive();

                if (isEntityActive)
                {
                    m_stateController.changeState(StateDefinition.BATTLE_CHARACTER_TURN_START);
                }
                else
                {
                    m_stateController.changeState(StateDefinition.BATTLE_TURN_END);
                }
            }
        }
    }

    protected float m_endTurnTimer;

}
