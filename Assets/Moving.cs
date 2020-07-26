using UnityEngine;

public class Moving : Istate
{
    private IEntity _entity;
    private IMove _entityMover;
    private IAnimate _animCont;
    private IRotate _entityRotation;
    private float TimeEnteredMoveState;
    private float TimeUntiLFullSpeed = 2f;
    public Moving(IEntity entity)
    {
        _entity = entity;
        _entityMover = _entity.EntityMover;
        _animCont = _entity.EntityanimCont;
        _entityRotation = entity.EntityRotation;
    }

    public void OnEnter()
    {
        _animCont. ControlLocomotionStepping(false);
           TimeEnteredMoveState = Time.time;
    }

    public void ONExit()
    {

    }

    public void OnTick()
    {
        if (_entity is character)
        {
            
            float timeSinceStartedMoving = (Time.time - TimeEnteredMoveState);
            float percentageComplete = (timeSinceStartedMoving / TimeUntiLFullSpeed) * Time.deltaTime;

            _animCont.SetWalkRunParameter(percentageComplete);
            _entity.EntityMover.MoveTick(_entity.inFightingDistance);

        }

        _entityRotation.RotationTick(_entity.Target, _entity.inFightingDistance);
    }
}
