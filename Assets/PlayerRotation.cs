using UnityEngine;

public class PlayerRotation : IRotate
{
    private Transform _entityTransform;
    private IControlPlayers _controller;
    private bool canPlayerRotate;
    public PlayerRotation(Transform entityTransform, IControlPlayers controller)
    {
        _entityTransform = entityTransform;
        _controller = controller;
        shouldRotate = true;
    }

    public bool shouldRotate { get { return canPlayerRotate; } set { canPlayerRotate = value; } }

    public void RotationTick(IEntity nearestEnemy, bool infightingDistance = false)
    {
        if (canPlayerRotate)
        {
            Vector3 DirectiontoTarget = infightingDistance ? nearestEnemy.transform.position - _entityTransform.position : _controller.getDirection();
            DirectiontoTarget.y = 0;
            Quaternion DirectionVec = Quaternion.LookRotation(DirectiontoTarget);
            //  Quaternion Rot = _entityTransform.rotation;
            _entityTransform.rotation = Quaternion.Slerp(_entityTransform.rotation, DirectionVec, 0.09f);
            //   _entityTransform.rotation = smoothTurn;
        }
    }

    //public void normalModeRotationTick()
    //{
    //    Vector3 dir = _controller.getDirection();
    //    if (dir != Vector3.zero)
    //    {
    //        Quaternion ForwardVector = Quaternion.LookRotation(_controller.getDirection());
    //        _entityTransform.rotation = Quaternion.Slerp(_entityTransform.rotation, ForwardVector, 0.1f);
    //    }

    //}
}
