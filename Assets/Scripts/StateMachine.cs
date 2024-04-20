using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            SetState(transition.toState);
        }

        currentState?.Tick();
    }

    public void SetState(IState state)
    {
        if (state == currentState)
        {
            return;
        }

        currentState?.OnExit();
        currentState = state;

        transitions.TryGetValue(currentState.GetType(), out currentTransitions);

        if (currentTransitions == null)
        {
            currentTransitions = EmptyTransitions;
        }

        currentState.OnEnter();
    }


    void AddTransition(IState current, IState destination, bool condition)
    {
        if (transitions.TryGetValue(current.GetType(), out var transitions) == false)
        {
            transitions = new List<Transition>();
            transitions[from.get]
        }
    }

    void AddAnyTransition(IState destination, bool condition)
    {

    }

    private class Transition
    {
        public bool condition;
        public IState toState;

        public Transition(IState destination, bool condition)
        {
            toState = destination;
            this.condition = condition;
        }
    }

    private Transition GetTransition()
    {
        foreach(var transition in anyTransitions)
        {
            if (transition.condition)
            {
                return transition;
            }
        }

        foreach(var transition in cur)
    }
}


