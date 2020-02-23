using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ApplicationController : MonoBehaviour
{
    public const float PIXELS_PER_UNIT = 16F;
    public const float PIXEL_SIZE = 1 / PIXELS_PER_UNIT;

    private List<Controller> m_controllers;

    public void Start()
    {
        m_controllers = FindObjectsOfType<Controller>().ToList();

        for (int i = 0; i < m_controllers.Count; i++)
        {
            m_controllers[i].initialize(this);
        }

        for (int i = 0; i < m_controllers.Count; i++)
        {
            m_controllers[i].onApplicationStart();
        }

    }



    public T getController<T>() where T : Controller
    {
        T l_controller = m_controllers.Find(x => x is T) as T;

        return l_controller;
    }
}
