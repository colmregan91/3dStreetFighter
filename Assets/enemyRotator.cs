using UnityEngine;

public class enemyRotator : IRotate
{
    private Transform _entityTransform;
    private bool shouldEnemyRotate;

    public enemyRotator (Transform transform)
    {
        _entityTransform = transform;
    }
    public bool shouldRotate { get { return shouldEnemyRotate; } set { shouldEnemyRotate = value; } }



    public void RotationTick(IEntity nearestEntity, bool infightingDistance = false)
    {
        if (shouldEnemyRotate)
        {
            Vector3 DirectiontoTarget = nearestEntity.transform.position - _entityTransform.position;
            DirectiontoTarget.y = 0;
            Quaternion DirectionVec = Quaternion.LookRotation(DirectiontoTarget);
            //  Quaternion Rot = _entityTransform.rotation;
            _entityTransform.rotation = Quaternion.Slerp(_entityTransform.rotation, DirectionVec, 0.1f);
            //   _entityTransform.rotation = smoothTurn;
        }
    }
}
