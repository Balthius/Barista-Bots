using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public virtual void initialize(ApplicationController p_controller)
    {
        m_controller = p_controller;
        m_viewController = m_controller.getController<ViewController>();
    }

    public virtual void onApplicationStart()
    {

    }

    public virtual void onApplicationClose()
    {

    }

    protected ApplicationController m_controller;
    protected ViewController m_viewController;
}
