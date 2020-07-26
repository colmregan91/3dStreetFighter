using System.Collections.Generic;
using System;
using UnityEngine;
public class StateMachine
{
    public Istate CurrentState { get; private set; }
    public event Action<Istate> HandleStateChange;
    public List<StateTransition> transitions = new List<StateTransition>();
    public List<StateTransition> transitionsFromAnyState = new List<StateTransition>();
    public void AddTransition (Istate from, Istate to, Func<bool> condition)
    {
        
        StateTransition transition = new StateTransition(from, to, condition);
        transitions.Add(transition);
    }
    public void addTransitionFromAnyState(Istate to, Func<bool> condition)
    {
        StateTransition newtransition = new StateTransition(null, to, condition);
        transitionsFromAnyState.Add(newtransition);
    }
    public void SetState(Istate state)
    {
        if (CurrentState == state)
        {
            return;
        }
        CurrentState?.ONExit();
        CurrentState = state;
        HandleStateChange?.Invoke(state);
        CurrentState.OnEnter();
    }

    public void Tick()
    {
        StateTransition trans = CHeckForTransition();
        if (trans != null)
        {
            SetState(trans.To);
        }
        CurrentState.OnTick();
    }

    StateTransition CHeckForTransition()
    {
        foreach (var trans in transitionsFromAnyState)
        {
            if (trans.Condition())
            {

                return trans;
            }
        }
        foreach (var transition in transitions)
        {
            if (CurrentState == transition.From && transition.Condition())
            {
                return transition;
            }
        }
        return null;
    }

}