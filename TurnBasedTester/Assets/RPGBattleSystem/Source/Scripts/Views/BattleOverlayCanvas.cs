using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class BattleOverlayCanvas : View
{
    [SerializeField]
    protected RectTransform m_topContent;
    [SerializeField]
    protected RectTransform m_bottomContent;

    [SerializeField]
    protected ElementConfig m_config;
    [SerializeField]
    protected Image m_entityPortrait;
    [SerializeField]
    protected UIEnergyBar m_entityHealth;
    [SerializeField]
    protected UIEnergyBar m_entityMana;
    [SerializeField]
    public List<UIAttackButton> m_attackButtons;

    [SerializeField]
    protected Text m_moderatorLabel;
    [SerializeField]
    protected GameObject m_console;
    [SerializeField]
    protected Text m_consoleLabel;

    [SerializeField]
    protected Button m_cancelSelectTargetButton;

    public event Action<AttackData> attackButtonPressed = delegate { };
    public event Action cancelButtonAction = delegate { };

    public override void open()
    {
        m_topContent.anchoredPosition = new Vector2(0f, m_topContent.sizeDelta.y);
        m_bottomContent.anchoredPosition = new Vector2(0f, -m_bottomContent.sizeDelta.y);

        m_topContent.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutQuart).SetDelay(1);
        m_bottomContent.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.OutQuart).SetDelay(1);

        for (int i = 0; i < m_attackButtons.Count; i++)
        {
            m_attackButtons[i].pressedAction += onAttackButtonPressed;
        }

        m_battleController = m_controller.getController<BattleController>();
        m_battleController.entityDeathAction += onEntityDeath;

        m_cancelSelectTargetButton.onClick.AddListener(onCancelSelectTargetButtonClicked);

        setCancelButtonActive(false);
    }

    public override void close()
    {
        m_battleController.entityDeathAction -= onEntityDeath;
        m_cancelSelectTargetButton.onClick.RemoveListener(onCancelSelectTargetButtonClicked);
        // calling base.close() will allow for the view to be destroyed from the scene.
        base.close();
    }

    public void setSelectedEntity(EntityState p_state)
    {
        m_entityPortrait.sprite = p_state.data.portrait;
        m_entityHealth.SetSliderValues(0, p_state.data.stats.health, p_state.currentHealth);
        m_entityMana.SetSliderValues(0, p_state.data.stats.mana, p_state.currentMana);

        for (int i = 0; i < m_attackButtons.Count; i++)
        {
            if (i >= p_state.data.attacks.Count)
            {
                m_attackButtons[i].setEmpty();
            }
            else
            {
                AttackData attackData = p_state.data.attacks[i].attackData;

                if (p_state.currentMana >= attackData.cost)
                {
                    m_attackButtons[i].initialize(p_state.data.attacks[i].attackData, m_config);
                }
                else
                {
                    m_attackButtons[i].initializeInsufficentMana(p_state.data.attacks[i].attackData, m_config);
                }

            }
        }
    }

    public void setModeratorLabel(string p_text)
    {
        m_moderatorLabel.text = p_text;
    }

    public void setConsoleLabel(string p_text)
    {
        m_console.SetActive(true);
        m_consoleLabel.text = p_text;
    }

    public void turnConsoleOff()
    {
        m_console.SetActive(false);
    }

    public void setCancelButtonActive(bool p_active)
    {
        m_cancelSelectTargetButton.gameObject.SetActive(p_active);
    }

    private void onEntityDeath(Entity p_entity)
    {
        string consoleOutput = string.Format("<color=#FF3F44FF>{0}</color> has been destroyed!", p_entity.state.data.name);
        setConsoleLabel(consoleOutput);
    }

    public void onAttackButtonPressed(AttackData p_data)
    {
        attackButtonPressed(p_data);
    }

    public void onCancelSelectTargetButtonClicked()
    {
        cancelButtonAction();
    }

    public void useMana(int cost)
    {
        m_entityMana.FillValue(-cost);
    }

    protected BattleController m_battleController;
}
