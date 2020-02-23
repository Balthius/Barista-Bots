using UnityEngine;
using UnityEngine.UI;
using System;

public class UICharacterSelectable : MonoBehaviour
{
    [SerializeField]
    protected Button m_button;
    [SerializeField]
    protected Image m_portrait;
    [SerializeField]
    protected GameObject m_selectedOverlay;

    public event Action<UICharacterSelectable> selectAction = delegate { };
    public event Action<UICharacterSelectable> deselectAction = delegate { };

    public EntityData data
    {
        get { return m_data; }
    }

    public void setData(EntityData p_data)
    {
        m_data = p_data;
        m_button.onClick.AddListener(select);
        m_selectedOverlay.SetActive(false);
        m_portrait.sprite = m_data.portrait;
    }

    public virtual void select()
    {
        m_button.onClick.RemoveListener(select);
        m_button.onClick.AddListener(deselect);

        m_selectedOverlay.SetActive(true);

        selectAction(this);
    }

    public virtual void deselect()
    {
        m_button.onClick.AddListener(select);
        m_button.onClick.RemoveListener(deselect);

        m_selectedOverlay.SetActive(false);

        deselectAction(this);
    }

    protected EntityData m_data;
}
