using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateController : Controller
{
    [SerializeField]
    protected Text m_debugLabel;

    public StateDefinition currentState
    {
        get { return m_currentState; }
    }

    public override void onApplicationStart()
    {
        m_states = new Dictionary<StateDefinition, State>();

        m_states.Add(StateDefinition.LOADING, new LoadingState(m_controller));
        m_states.Add(StateDefinition.MAIN_MENU, new MainMenuState(m_controller));
        m_states.Add(StateDefinition.CHARACTER_SELECT, new CharacterSelectState(m_controller));

        m_states.Add(StateDefinition.BATTLE_START, new BattleStartState(m_controller));
        m_states.Add(StateDefinition.BATTLE_TURN_START, new BattleTurnStartState(m_controller));
        m_states.Add(StateDefinition.BATTLE_CHARACTER_TURN_START, new BattleCharacterStartTurnState(m_controller));
        m_states.Add(StateDefinition.BATTLE_WAIT_FOR_PLAYER_ACTION, new BattleWaitForPlayerActionState(m_controller));
        m_states.Add(StateDefinition.BATTLE_WAIT_FOR_AI_ACTION, new BattleWaitForAIActionState(m_controller));
        m_states.Add(StateDefinition.BATTLE_WAIT_SELECT_TARGET, new BattleWaitForSelectTargetState(m_controller));
        m_states.Add(StateDefinition.BATTLE_EXECUTE_ACTION, new BattleExecuteAttackState(m_controller));
        m_states.Add(StateDefinition.BATTLE_CHARACTER_TURN_END, new BattleCharacterEndTurnState(m_controller));
        m_states.Add(StateDefinition.BATTLE_TURN_END, new BattleTurnEndState(m_controller));
        m_states.Add(StateDefinition.BATTLE_WON, new BattleWonState(m_controller));
        m_states.Add(StateDefinition.BATTLE_LOSE, new BattleLoseState(m_controller));
        m_states.Add(StateDefinition.BATTLE_END, new BattleEndState(m_controller));

        changeState(StateDefinition.LOADING);

    }

    private void Update()
    {
        if (m_states.ContainsKey(m_currentState))
        {
            // if using your own time change Time.deltaTime to use your own time step!
            m_states[m_currentState].update(Time.deltaTime);
        }

        if (m_debugLabel != null)
        {
            m_debugLabel.text = string.Format("State: {0}", m_currentState);
        }
    }

    public void changeState(StateDefinition p_state)
    {
        if (m_states.ContainsKey(m_currentState))
        {
            m_states[m_currentState].exit();
        }
        m_currentState = p_state;

        if (m_states.ContainsKey(p_state))
        {
            m_states[m_currentState].enter();
        }
    }
    private StateDefinition m_currentState;
    private Dictionary<StateDefinition, State> m_states;

}
