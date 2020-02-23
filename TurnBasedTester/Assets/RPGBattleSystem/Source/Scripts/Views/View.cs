using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    public virtual void initialize(ApplicationController p_controller)
    {
        m_controller = p_controller;
    }

    public virtual void open()
    {

    }

    public virtual void update(float p_delta)
    {

    }

    public virtual void close()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        update(Time.deltaTime);
    }

    protected ApplicationController m_controller;
}
