using System.Collections.Generic;
using UnityEngine;

public class ViewController : Controller
{
    [SerializeField]
    protected Camera m_viewCamera;

    public override void initialize(ApplicationController p_controller)
    {
        base.initialize(p_controller);

        m_views = new Dictionary<ViewDefinition, Transform>();
        m_open = new Dictionary<ViewDefinition, View>();

        m_views.Add(ViewDefinition.BATTLE_OVERLAY_MENU, Resources.Load<Transform>("Views/BattleOverlayCanvas"));
        m_views.Add(ViewDefinition.BATTLE_END_MENU, Resources.Load<Transform>("Views/BattleEndMenu"));
        m_views.Add(ViewDefinition.CHARACTER_SELECT_SCREEN, Resources.Load<Transform>("Views/CharacterSelectScreen"));
    }

    public T open<T>(ViewDefinition p_view) where T : View
    {
        T view = null;

        if (m_open.ContainsKey(p_view))
        {
            view = m_open[p_view] as T;
        }
        else
        {
            Transform viewTransform = Instantiate(m_views[p_view]);
            viewTransform.SetParent(transform, false);
            view = viewTransform.GetComponent<T>();
            view.initialize(m_controller);
            view.open();

            Canvas canvas = view.GetComponent<Canvas>();

            if (canvas != null)
            {
                canvas.worldCamera = m_viewCamera;
            }

            m_open.Add(p_view, view);
        }

        return view;
    }

    public void close(ViewDefinition p_view)
    {
        if (m_open.ContainsKey(p_view))
        {
            View view = m_open[p_view];
            m_open.Remove(p_view);
            view.close();
        }
    }

    protected Dictionary<ViewDefinition, Transform> m_views;
    protected Dictionary<ViewDefinition, View> m_open;
}
