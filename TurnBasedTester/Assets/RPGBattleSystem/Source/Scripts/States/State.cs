

public class State
{
    public State(ApplicationController p_controller)
    {
        m_controller = p_controller;
        m_stateController = m_controller.getController<StateController>();
        m_viewController = m_controller.getController<ViewController>();
    }

    public virtual void enter()
    {

    }

    public virtual void update(float p_delta)
    {

    }

    public virtual void exit()
    {

    }

    protected ApplicationController m_controller;
    protected StateController m_stateController;
    protected ViewController m_viewController;

}
