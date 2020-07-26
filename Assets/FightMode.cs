using UnityEngine;
public class FightMode : Istate
{
    private IEntity _entity;
    private IMove _Charactermover;
    private IAnimate _AnimCont;
    private IRotate _rotator;

    public FightMode(IEntity Entity)
    {
        _entity = Entity;
        _Charactermover = _entity.EntityMover;
        _AnimCont = _entity.EntityanimCont;
        _rotator = _entity.EntityRotation;
    }
    public void OnEnter()
    {

        _AnimCont.ControlIdleAnimParams(true);
    }

    public void ONExit()
    {
        _AnimCont.ControlIdleAnimParams(false);
    }

    public void OnTick()
    {


        if (_entity is character)
        {
            _AnimCont.SetWalkRunParameter(1);
            _AnimCont.ControlLocomotionStepping();
            _entity.EntityMover.MoveTick(_entity.inFightingDistance);
            _entity.LerpCont.getInput();
        }

        _rotator.RotationTick(_entity.Target, _entity.inFightingDistance);
     
    }
}
