using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System;


public class CharacterSelectScreen : View
{
    [SerializeField]
    protected LayoutGroup m_characterLayout;
    [SerializeField]
    protected CanvasGroup m_canvasGroup;
    [SerializeField]
    protected Button m_continueButton;
    [SerializeField]
    protected RectTransform m_characterButtonPrefab;
    [SerializeField]
    protected List<EntityData> m_characterData;
    [SerializeField]
    protected List<EntityData> m_monsterData;
    [SerializeField]
    protected List<EnvironmentData> m_environmentData;

    public event Action continueButtonAction = delegate { };

    public List<UICharacterSelectable> selectedCharacters
    {
        get { return m_selectedCharacters; }
    }

    public List<EntityData> monsterData
    {
        get { return m_monsterData; }
    }

    public List<EnvironmentData> environmentData
    {
        get { return m_environmentData; }
    }

    public override void open()
    {
        m_selectedCharacters = new List<UICharacterSelectable>();

        m_canvasGroup.blocksRaycasts = true;
        m_canvasGroup.alpha = 1;
        m_canvasGroup.DOFade(0, 0.5f).OnComplete(() => m_canvasGroup.blocksRaycasts = false);

        m_continueButton.onClick.AddListener(OnContinueButton);

        for (int i = 0; i < m_characterData.Count; i++)
        {
            RectTransform characterButton = Instantiate(m_characterButtonPrefab);
            characterButton.SetParent(m_characterLayout.transform, false);
            UICharacterSelectable characterSelectable = characterButton.GetComponent<UICharacterSelectable>();
            characterSelectable.setData(m_characterData[i]);
            characterSelectable.selectAction += OnCharacterSelected;
            characterSelectable.deselectAction += OnCharacterDeselected;
        }

        updateUI();
    }

    public override void close()
    {
        for (int i = 0; i < m_characterLayout.transform.childCount; i++)
        {
            Transform characterButton = m_characterLayout.transform.GetChild(i);
            UICharacterSelectable characterSelectable = characterButton.GetComponent<UICharacterSelectable>();
            characterSelectable.selectAction -= OnCharacterSelected;
            characterSelectable.deselectAction -= OnCharacterDeselected;

        }

        m_continueButton.onClick.RemoveListener(OnContinueButton);

        m_canvasGroup.blocksRaycasts = true;
        m_canvasGroup.alpha = 0;
        m_canvasGroup.DOFade(1, 0.5f).OnComplete(base.close);
    }

    private void updateUI()
    {
        m_continueButton.interactable = m_selectedCharacters.Count >= 2;
    }

    private void OnCharacterSelected(UICharacterSelectable p_selectable)
    {
        if (m_selectedCharacters.Count >= 2)
        {
            UICharacterSelectable removal = m_selectedCharacters[0];
            removal.deselect();
            m_selectedCharacters.Remove(removal);
        }

        m_selectedCharacters.Add(p_selectable);

        updateUI();
    }

    private void OnCharacterDeselected(UICharacterSelectable p_selectable)
    {
        if (m_selectedCharacters.Contains(p_selectable))
        {
            m_selectedCharacters.Remove(p_selectable);
        }

        updateUI();
    }

    private void OnContinueButton()
    {
        continueButtonAction();
    }

    protected List<UICharacterSelectable> m_selectedCharacters;
}
