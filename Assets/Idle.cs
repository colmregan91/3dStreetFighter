using UnityEngine;

public class Idle : Istate
{
    private IMove _CharacterMover;
    private IAnimate _animCont;
    private float TimeEnteredIdleState;
    private float TimeUntilFullyStopped = 2f;
    public Idle(IEntity entity)
    {
        _CharacterMover = entity.EntityMover;
           _animCont = entity.EntityanimCont;
    }
    public void OnEnter()
    {
        TimeEnteredIdleState = Time.time;
    }

    public void ONExit()
    {

    }

    public void OnTick()
    {
        float timeSinceStartedSlowingDown = (Time.time - TimeEnteredIdleState);
        float percentageComplete = (timeSinceStartedSlowingDown / TimeUntilFullyStopped) * Time.deltaTime * 100;

        _animCont.SetWalkRunParameter(percentageComplete);
        _CharacterMover.MoveTick(false);
    }
}
