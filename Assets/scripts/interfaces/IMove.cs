
using UnityEngine;

public interface IMove
{
    Transform entityTransform { get; }
    float moveSpeed { get; set; }
    float fightMoveSpeed { get; }
    Vector3 getDirection { get; }

    void MoveTick(bool fightMode);
    bool isEntityMoving();
    bool CanEntityMove { get; set; }
    void SetCanMove();
    void SetCanNOTMove();

}

