using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIAttackButton : MonoBehaviour
{
    [SerializeField]
    protected Button m_button;
    [SerializeField]
    protected Sprite m_emptyAttackIcon;
    [SerializeField]
    protected Image m_attackIcon;
    [SerializeField]
    protected Text m_attackName;
    [SerializeField]
    protected Text m_attackDescription;
    [SerializeField]
    protected Text m_attackPower;
    [SerializeField]
    protected Image m_elementIcon;
    [SerializeField]
    protected Text m_manaCost;

    public event Action<AttackData> pressedAction = delegate { };

    public virtual void initialize(AttackData p_data, ElementConfig p_elementConfig)
    {
        m_attackData = p_data;
        m_elementData = p_elementConfig.getElementData(m_attackData.element);

        m_button.interactable = true;

        m_attackIcon.gameObject.SetActive(true);
        m_attackName.gameObject.SetActive(true);
        m_manaCost.gameObject.SetActive(true);

        m_attackIcon.sprite = m_attackData.portrait;
        m_attackName.text = m_attackData.name;

        m_manaCost.color = "8E4A81FF".ToColor();
        m_manaCost.text = m_attackData.cost.ToString();

        if (m_attackData.attackType == AttackType.MELEE || m_attackData.attackType == AttackType.RANGED)
        {
            m_attackPower.gameObject.SetActive(true);
            m_elementIcon.gameObject.SetActive(true);
            m_attackDescription.gameObject.SetActive(false);

            m_attackPower.text = m_attackData.power.ToString();
            m_elementIcon.sprite = m_elementData.icon;

        }
        else
        {
            m_attackPower.gameObject.SetActive(false);
            m_elementIcon.gameObject.SetActive(false);
            m_attackDescription.gameObject.SetActive(true);

            m_attackDescription.text = m_attackData.getDisplayDescription();
        }

        m_button.onClick.RemoveAllListeners();
        m_button.onClick.AddListener(onClick);
    }

    public virtual void initializeInsufficentMana(AttackData p_data, ElementConfig p_elementConfig)
    {
        initialize(p_data, p_elementConfig);

        m_manaCost.color = "FF3F44FF".ToColor();

        m_button.onClick.RemoveAllListeners();
        m_button.onClick.AddListener(onClickInsufficentMana);
    }

    public virtual void setEmpty()
    {
        m_button.interactable = false;
        m_attackIcon.sprite = m_emptyAttackIcon;
        m_attackName.text = "-";

        m_attackPower.gameObject.SetActive(false);
        m_manaCost.gameObject.SetActive(false);

        m_attackPower.gameObject.SetActive(false);
        m_elementIcon.gameObject.SetActive(false);
        m_attackDescription.gameObject.SetActive(false);

    }

    private void onClick()
    {
        pressedAction(m_attackData);
    }

    private void onClickInsufficentMana()
    {
        Debug.LogWarningFormat("do something when the player selects an attack with not enough mana to show feedback.");
    }


    protected AttackData m_attackData;
    protected ElementData m_elementData;
}
