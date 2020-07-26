using UnityEngine;
using System;
using System.Collections;

public class DodgeController : ILerp
{
    public IAnimate EntityanimCont { get; private set; }
    public IControlPlayers Controller { get; private set; }

    private IEntity entity;
    private Vector3 targetPos;
    public bool isLerping { get; set; }
    public DodgeController(IEntity Entity, IControlPlayers control)
    {
        entity = Entity;
        EntityanimCont = entity.EntityanimCont;
        Controller = control;
    }
    public void getInput()
    {
        if (Controller.Square_Button())
        {
           SetLerp();
        }
    }
    public void SetLerp()
    {
        if (isLerping) return;

        isLerping = true;
        EntityanimCont.SetLerpTrigger();
    }
    public void ResetLerp()
    {
        isLerping = false;
    }
    public Vector3 GetTargetPos()
    {
        Vector3 Behinddir = entity.EntityMover.isEntityMoving() ? Controller.getDirection() * 1.1f : -entity.transform.forward;
        Vector3 TargetPos = entity.transform.position + Behinddir;
        return TargetPos;
    }
    public void LerpTick(Vector3 targetpos, float percentComplete)
    {
        entity.transform.position = Vector3.Lerp(entity.transform.position, targetpos, percentComplete);
    }
}

public interface ILerp
{
    bool isLerping { get; set; }
    void getInput();
    void SetLerp();
    void ResetLerp();
    Vector3 GetTargetPos();
    void LerpTick(Vector3 targetpos, float percentComplete);

}
