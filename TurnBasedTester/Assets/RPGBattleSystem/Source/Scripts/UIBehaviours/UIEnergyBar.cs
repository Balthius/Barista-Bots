using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIEnergyBar : MonoBehaviour
{
    #region Members

    [SerializeField]
    private Slider m_slider;

    [SerializeField]
    private UISpriteText m_label;
    [SerializeField]
    private string m_format = "{0}/{1}";

    [SerializeField]
    private int m_minValue;
    [SerializeField]
    private int m_maxValue;

    [SerializeField]
    private float m_fillDuration = 0.5f;

    [SerializeField]
    private Ease m_fillEase;
    [SerializeField]
    private Ease m_unfillEase;

    private int m_value;

    #endregion

    #region Properties

    public virtual int value
    {
        get { return m_value; }
    }

    public virtual int maxValue
    {
        get { return m_maxValue; }
    }

    public virtual int minValue
    {
        get { return m_minValue; }
    }

    public virtual Slider slider
    {
        get { return m_slider; }
    }

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        //RefreshLabel(0);
        //RefreshSlider();
    }

    #endregion

    #region Public Methods

    public virtual void SetSliderValues(int p_min, int p_max, int p_value)
    {
        m_minValue = p_min;
        m_maxValue = p_max;
        m_value = p_value;

        RefreshSlider();
    }

    public virtual void RefreshLabel(int p_value)
    {
        m_value = p_value;

        if (m_label == null)
            return;

        m_label.text = string.Format(m_format, p_value, m_maxValue, m_minValue);
    }

    public virtual void RefreshSlider()
    {
        if (m_slider == null)
            return;

        m_slider.minValue = m_minValue;
        m_slider.maxValue = m_maxValue;
        m_slider.value = m_value;

        RefreshLabel(m_value);
    }

    public virtual void FillValue(int p_value, Action onComplete = null)
    {
        int clampedValue = Mathf.Clamp(m_value + p_value, m_minValue, m_maxValue);
        float duration = m_fillDuration;
        Ease ease = clampedValue > m_value ? m_fillEase : m_unfillEase;

        m_value = clampedValue;

        m_slider.DOValue(m_value, duration)
            .SetEase(ease)
            .OnUpdate(() => RefreshLabel((int)m_slider.value))
            .OnComplete(() =>
            {
                if (onComplete != null)
                    onComplete();
            });
    }

    public virtual Tweener FillValueTween(int p_value)
    {
        int clampedValue = Mathf.Clamp(m_value + p_value, m_minValue, m_maxValue);
        float duration = m_fillDuration;
        Ease ease = clampedValue > m_value ? m_fillEase : m_unfillEase;

        m_value = clampedValue;

        Tweener l_tween = m_slider.DOValue(m_value, duration)
            .SetEase(ease)
            .OnUpdate(() => RefreshLabel((int)m_slider.value));

        return l_tween;
    }

    public virtual void FillValue(int p_value, Action<int> p_overflow)
    {
        int overflow = 0;

        if (m_value + p_value > m_maxValue)
        {
            overflow = (m_value + p_value) - m_maxValue;
        }

        int clampedValue = Mathf.Clamp(m_value + p_value, m_minValue, m_maxValue);
        float duration = m_fillDuration;
        Ease ease = clampedValue > m_value ? m_fillEase : m_unfillEase;

        m_value = clampedValue;

        m_slider.DOValue(m_value, duration)
            .SetEase(ease)
            .OnUpdate(() => RefreshLabel((int)m_slider.value))
            .OnComplete(() =>
            {
                if (overflow > 0)
                    p_overflow(overflow);
            });
    }

    #endregion


}
