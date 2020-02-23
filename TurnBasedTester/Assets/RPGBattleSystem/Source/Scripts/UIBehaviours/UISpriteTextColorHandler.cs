using UnityEngine;
using UnityEngine.EventSystems;

public class UISpriteTextColorHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,
                                        IPointerExitHandler, IPointerEnterHandler
{

    protected UISpriteText m_spriteText;
    [SerializeField]
    protected Color m_normalColor;
    [SerializeField]
    protected Color m_downColor;
    [SerializeField]
    protected Color m_hoverColor;

    private void Start()
    {
        m_spriteText = GetComponentInChildren<UISpriteText>();

        if (m_spriteText)
        {
            m_spriteText.setColor(m_normalColor);
        }

    }

    public void OnPointerDown(PointerEventData data)
    {
        if (m_spriteText)
        {
            m_spriteText.setColor(m_downColor);
        }

    }

    public void OnPointerUp(PointerEventData data)
    {
        if (m_spriteText)
        {
            if (Application.isMobilePlatform)
            {
                m_spriteText.setColor(m_normalColor);
            }
            else
            {
                m_spriteText.setColor(m_hoverColor);
            }
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (m_spriteText)
        {
            m_spriteText.setColor(m_normalColor);
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (m_spriteText)
        {
            m_spriteText.setColor(m_hoverColor);
        }
    }
}
