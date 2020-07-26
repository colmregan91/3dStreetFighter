using System;

public class StateTransition
{
    public Istate From;
    public Istate To;
    public Func<bool> Condition { get; }

    public StateTransition (Istate from, Istate to,Func<bool> condition)
    {
        From = from;
        To = to;
        Condition = condition;
    }
}
