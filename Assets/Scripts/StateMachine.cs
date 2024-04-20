using System;
using System.Collections.Generic;

public class StateMachine
{
    Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();
    List<Transition> currentTransitions = new List<Transition>();
    List<Transition> anyTransitions = new List<Transition>();

    static List<Transition> EmptyTransitions = new List<Transition>();

    IState currentState;
    
    public void Tick()
    {
        var transition = GetTransition();
        if (transition != null)
        {
            SetState(transition.ToState);
        }

        currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == currentState) return;

        currentState?.OnExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);

        if (currentTransitions == null) currentTransitions = EmptyTransitions;

        currentState.OnEnter();
    }

    public void AddTransition(IState from, IState to, bool condition)
    {
        if (transitions.TryGetValue(from.GetType(), out var trans) == false)
        {
            trans = new List<Transition>();
            transitions[from.GetType()] = trans;
        }

        trans.Add(new Transition(to, condition));
    }

    public void AddAnyTransition(IState destination, bool condition)
    {
        anyTransitions.Add(new Transition(destination, condition));
    }

    private class Transition
    {
        public bool Condition;
        public IState ToState;

        public Transition(IState destination, bool condition)
        {
            ToState = destination;
            Condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach(var transition in anyTransitions)
        {
            if (transition.Condition) return transition;
        }

        foreach(var transition in currentTransitions)
        {
            if (transition.Condition) return transition;
        }

        return null;
    }
}


