using UnityEngine;

public class enemyImpactMover : ILerp
{
    private IEntity _entity;
    private IAnimate EntityanimCont;

    public enemyImpactMover (IEntity entity)
    {
        _entity = entity;
        EntityanimCont = _entity.EntityanimCont;
    }
    public bool isLerping { get; set; }
    public void LerpTick(Vector3 targetpos, float percentComplete)
    {
        _entity.transform.position = Vector3.Lerp(_entity.transform.position, targetpos, percentComplete);

    }

    public void getInput()
    {

    }

    public Vector3 GetTargetPos()
    {
     
           Vector3 Behinddir = -_entity.transform.forward * 2f;
        Vector3 TargetPos = _entity.transform.position + Behinddir;
        return TargetPos;
    }

    public void ResetLerp()
    {
        isLerping = false;
    }

    public void SetLerp()
    {
        if (isLerping) return;
  
        isLerping = true;
        EntityanimCont.SetLerpTrigger();
    }
}
