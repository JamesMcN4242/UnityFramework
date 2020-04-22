////////////////////////////////////////////////////////////
/////   FSStateBase.cs
/////   James McNeil - 2020
////////////////////////////////////////////////////////////

/// <summary>
/// State base for controlling logical states
/// </summary>
public class FSStateBase
{ 
    private enum Status {START_PRESENTING, PRESENTING, START_ACTIVE, ACTIVE, START_DISMISSING, DISMISSING, DISMISSED};
    private Status m_status = Status.START_PRESENTING;
    protected UIStateBase m_ui = null;

    public void UpdateState()
    {
        switch(m_status)
        {
            case Status.START_PRESENTING:
                BeginPresentingState();
                break;

            case Status.PRESENTING:
                UpdatePresentingState();
                break;

            case Status.START_ACTIVE:
                BeginActiveState();
                break;
            
            case Status.ACTIVE:
                UpdateActiveState();
                break;
            
            case Status.START_DISMISSING:
                BeginDismissingState();
                break;

            case Status.DISMISSING:
                UpdateDismissingState();
                break;
        }
    }

    public virtual void HandleMessage(string message)
    {
    }
    
    protected virtual bool AquireUIFromScene()
    {
        return false;
    }

    /// <summary>
    /// When the state starts it's presenting state
    /// </summary>
    private void BeginPresentingState()
    {
        if(AquireUIFromScene())
        {
            m_ui.SetContentActiveStatus(true);
        }

        StartPresentingState();
        m_status = Status.PRESENTING;
    }

    protected virtual void StartPresentingState()
    {
    }

    /// <summary>
    /// Update the presenting state of the state
    /// </summary>
    protected virtual void UpdatePresentingState()
    {
        EndPresentingState();
    }

    /// <summary>
    /// When the state enters it's active state
    /// </summary>
    private void BeginActiveState()
    {
        StartActiveState();
        m_status = Status.ACTIVE;
    }

    protected virtual void StartActiveState()
    {
    }

    /// <summary>
    /// Update the active state
    /// </summary>
    protected virtual void UpdateActiveState()
    {
    }

    private void BeginDismissingState()
    {
        StartDismissingState();
        m_status = Status.DISMISSING;
    }

    protected virtual void StartDismissingState()
    {
    }

    protected virtual void UpdateDismissingState()
    {
        EndDismissingState();
    }
    

    protected void EndPresentingState()
    {
        m_status = Status.START_ACTIVE;
    }

    /// <summary>
    /// When the state ends it's active state
    /// </summary>
    public void EndActiveState()
    {
        m_status = Status.START_DISMISSING;
    }

    public void EndDismissingState()
    {
        m_status = Status.DISMISSED;
        if(m_ui != null)
        {
            m_ui.SetContentActiveStatus(false);
        }
    }

    public bool IsDismissed()
    {
        return m_status == Status.DISMISSED;
    }
}