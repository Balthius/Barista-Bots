using UnityEngine;
using UnityEngine.UI;

public class EnvironmentSetting : MonoBehaviour
{
    [SerializeField]
    protected Image m_background;
    [SerializeField]
    protected UISpriteText m_nameLabel;
    [SerializeField]
    protected AudioSource m_audioSource;

    public virtual void initialize(EnvironmentData p_data)
    {
        m_data = p_data;

        m_nameLabel.text = m_data.name;
        m_background.sprite = m_data.background;
        m_audioSource.clip = m_data.music;
        m_audioSource.Play();

    }

    protected EnvironmentData m_data;
}
