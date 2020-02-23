using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreen : Controller
{
    private float m_force;
    private bool m_shaking;
    private float m_val;

    private Vector3 m_originalPosition;

    public override void initialize(ApplicationController p_controller)
    {
        base.initialize(p_controller);

        m_force = 0;
        m_shaking = false;

        m_originalPosition = transform.position;
    }

    public virtual void shake()
    {
        m_force = 5f * ApplicationController.PIXEL_SIZE;
        m_shaking = true;
        m_val = Mathf.PI / 2;
    }

    public virtual void Update()
    {
        if (m_shaking)
        {
            transform.position = new Vector3(m_originalPosition.x,
                                             m_originalPosition.y + (Mathf.Sin(m_val) * m_force),
                                             m_originalPosition.z);
            m_val += 1.5f;
            m_force -= 0.1f * ApplicationController.PIXEL_SIZE;
            if (m_force <= 0)
            {
                transform.position = m_originalPosition;
                m_shaking = false;
            }

        }
    }
}
