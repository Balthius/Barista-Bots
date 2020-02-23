using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectState : State
{
    public CharacterSelectState(ApplicationController p_controller) : base(p_controller) { }

    public override void enter()
    {
        m_battleController = m_controller.getController<BattleController>();
        m_characterSelectScreen = m_viewController.open<CharacterSelectScreen>(ViewDefinition.CHARACTER_SELECT_SCREEN);
        m_characterSelectScreen.continueButtonAction += OnContinueButton;
    }

    public void OnContinueButton()
    {
        m_characterSelectScreen.continueButtonAction -= OnContinueButton;

        BattleSettings settings = new BattleSettings();

        for (int i = 0; i < m_characterSelectScreen.selectedCharacters.Count; i++)
        {
            UICharacterSelectable character = m_characterSelectScreen.selectedCharacters[i];
            settings.entities.Add(character.data);
        }

        settings.entities.Add(m_characterSelectScreen.monsterData[Random.Range(0, m_characterSelectScreen.monsterData.Count)]);
        settings.entities.Add(m_characterSelectScreen.monsterData[Random.Range(0, m_characterSelectScreen.monsterData.Count)]);

        settings.environment = m_characterSelectScreen.environmentData[Random.Range(0, m_characterSelectScreen.environmentData.Count)];

        m_battleController.setBattleSettings(settings);
        m_viewController.close(ViewDefinition.CHARACTER_SELECT_SCREEN);
        m_stateController.changeState(StateDefinition.BATTLE_START);
    }

    protected BattleController m_battleController;
    protected CharacterSelectScreen m_characterSelectScreen;
}
