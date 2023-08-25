using System;
using System.Collections.Generic;

public class StateMachine
{
    public IAgent m_Agent;
    public Dictionary<int, State> m_StateDict = new();
    public State m_CurrState;
    public event Action<State, State> Changed;

    public StateMachine(IAgent agent)
    {
        m_Agent = agent;
    }

    public void SetDefault(int key)
    {
        if (m_StateDict.TryGetValue(key, out m_CurrState))
        {
            //Changed.Invoke(m_CurrState, m_CurrState);
            m_CurrState.Enter();
        }
    }

    public void AddStates(State[] states)
    {
        foreach (State state in states)
        {
            AddState(state);
        }
    }

    public void AddState(State state)
    {
        state.m_Agent = m_Agent;
        state.SetStateMachine(this);
        m_StateDict[state.m_StateType] = state;
    }

    public State Transition(int key)
    {
        m_StateDict.TryGetValue(key, out State s);
        return s;
    }

    public void Update()
    {
        if (m_CurrState == null) return;

        State nextState = m_CurrState.Execute();

        if (nextState != m_CurrState)
        {
            m_CurrState.Exit(); // 先退后进

            Changed.Invoke(m_CurrState, nextState);

            m_CurrState = nextState;
            m_CurrState?.Enter();
        }
    }
}
