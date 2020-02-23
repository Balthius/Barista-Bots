
public class BattleWonState : BattleState
{
    public BattleWonState(ApplicationController p_controller)
        : base(p_controller)
    {

    }

    public override void enter()
    {
        m_battleEndMenu = m_viewController.open<BattleEndMenu>(ViewDefinition.BATTLE_END_MENU);

        m_battleEndMenu.setStatusLabel("You Win!!");
        m_battleEndMenu.backButtonAction += OnBackButton;
        m_battleEndMenu.restartButtonAction += OnRestartButton;
    }

    public override void exit()
    {
        m_battleEndMenu.backButtonAction -= OnBackButton;
        m_battleEndMenu.restartButtonAction -= OnRestartButton;

        m_viewController.close(ViewDefinition.BATTLE_END_MENU);
    }

    private void OnBackButton()
    {
        m_stateController.changeState(StateDefinition.CHARACTER_SELECT);
    }

    private void OnRestartButton()
    {
        m_stateController.changeState(StateDefinition.BATTLE_START);
    }


    protected BattleEndMenu m_battleEndMenu;
}
