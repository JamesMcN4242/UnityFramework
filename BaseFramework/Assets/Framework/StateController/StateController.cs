////////////////////////////////////////////////////////////
/////   StateController.cs
/////   James McNeil - 2020
////////////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

namespace PersonalFramework
{
    public class StateController
    {
        private readonly Stack<FlowStateBase> m_stateStack = new Stack<FlowStateBase>();
        private FlowStateBase m_stateToChangeTo;

        public void PushState(FlowStateBase state)
        {
            Debug.Assert(m_stateStack.Count == 0 || m_stateStack.Peek() != state, "Trying to push already active state");
            m_stateStack.Push(state);
            state.SetStateController(this);
        }

        public void PopState()
        {
            Debug.Assert(m_stateStack.Count > 0, "Trying to pop a state which doesn't exist");
            m_stateStack.Peek().EndActiveState();
        }

        public void ChangeState(FlowStateBase state)
        {
            Debug.Assert(m_stateToChangeTo == null, "Trying to change state, when one is already specified.");
            m_stateToChangeTo = state;
            PopState();
        }

        public void UpdateStack()
        {
            if (m_stateStack.Count > 0)
            {
                FlowStateBase state = m_stateStack.Peek();
                state.UpdateState();
                if (state.IsDismissed())
                {
                    m_stateStack.Pop();

                    if (m_stateToChangeTo != null)
                    {
                        m_stateStack.Push(m_stateToChangeTo);
                        m_stateToChangeTo = null;
                    }
                }
            }
        }

        public void FixedUpdateStack()
        {
            if (m_stateStack.Count > 0)
            {
                FlowStateBase state = m_stateStack.Peek();
                state.FixedUpdateState();
            }
        }
    }
}