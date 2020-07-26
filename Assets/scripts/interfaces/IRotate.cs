using UnityEngine;
public interface IRotate
{

    bool shouldRotate { get; set; }
    void RotationTick(IEntity nearestEnemy, bool inFightingDistance = false);

}

