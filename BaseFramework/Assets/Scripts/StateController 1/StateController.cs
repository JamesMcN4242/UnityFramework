////////////////////////////////////////////////////////////
/////   StateController.cs
/////   James McNeil - 2020
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class StateController
{
    private Stack<FSStateBase> m_stateStack = new Stack<FSStateBase>();

    public void PushState(FSStateBase state)
    {        
        Debug.Assert(m_stateStack.Count == 0 || m_stateStack.Peek() != state, "Trying to push already active state");
        m_stateStack.Push(state);        
    }

    public void PopState(FSStateBase state)
    {
        Debug.Assert(m_stateStack.Count > 0 && m_stateStack.Peek() == state, "Trying to pop non active state");
        m_stateStack.Peek().EndActiveState();
    }

    public void UpdateStack()
    {
        if(m_stateStack.Count > 0)
        {
            FSStateBase state = m_stateStack.Peek();
            state.UpdateState();
            if(state.IsDismissed())
            {
                m_stateStack.Pop();
            }
        }
    }

    public void HandleMessage(string message)
    {
        if(m_stateStack.Count > 0)
        {
            m_stateStack.Peek().HandleMessage(message);
        }
    }
}
