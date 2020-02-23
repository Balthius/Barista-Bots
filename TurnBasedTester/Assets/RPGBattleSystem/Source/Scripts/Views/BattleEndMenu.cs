using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class BattleEndMenu : View
{
    [SerializeField]
    protected RectTransform m_mask;
    [SerializeField]
    protected RectTransform m_menuContent;
    [SerializeField]
    protected Text m_statusLabel;
    [SerializeField]
    protected Button m_backButton;
    [SerializeField]
    protected Button m_restartButton;

    public event Action backButtonAction = delegate { };
    public event Action restartButtonAction = delegate { };

    public override void open()
    {
        Vector2 size = m_mask.sizeDelta;
        m_mask.sizeDelta = new Vector2(m_mask.sizeDelta.x, 0f);
        m_mask.DOSizeDelta(size, 1f).SetEase(Ease.OutBack);

        m_backButton.onClick.AddListener(OnBackButton);
        m_restartButton.onClick.AddListener(OnRestartButton);
    }

    public override void close()
    {
        m_backButton.onClick.RemoveListener(OnBackButton);
        m_restartButton.onClick.RemoveListener(OnRestartButton);

        Vector2 size = new Vector2(m_mask.sizeDelta.x, 0f);
        m_mask.DOSizeDelta(size, 1f)
              .SetEase(Ease.InBack)
              .OnComplete(base.close);
    }

    public void setStatusLabel(string p_status)
    {
        m_statusLabel.text = p_status;
    }

    private void OnBackButton()
    {
        backButtonAction();
    }

    private void OnRestartButton()
    {
        restartButtonAction();
    }
}
