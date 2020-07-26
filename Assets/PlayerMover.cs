using UnityEngine;

public class PlayerMover : IMove
{
    private IControlPlayers Cont;
    private Transform EntityTransform;
    private float MoveSpeed = 2.5f;
    private float FightMoveSpeed = 1.5f;
    private bool CanMove;
    public Transform entityTransform { get { return EntityTransform; } }

    public float moveSpeed { get { return MoveSpeed; } set { MoveSpeed = value; } }
    public float fightMoveSpeed { get { return FightMoveSpeed; } }

    public Vector3 getDirection { get { return Cont.getDirection(); } }
    public bool isEntityMoving() => Cont.getDirection().magnitude > 0f;
    public bool CanEntityMove { get { return CanMove; } set { CanMove = value; } }
    public PlayerMover(IControlPlayers Cont, Transform entityTransform)
    {
        this.Cont = Cont;
        EntityTransform = entityTransform;
    }

    public void MoveTick(bool infightingDistance)
    {
        float speed = infightingDistance ? fightMoveSpeed : moveSpeed;

        if (CanEntityMove && Cont.getDirection().magnitude > 0.1)
            EntityTransform.position += Cont.getDirection() * Time.deltaTime * speed;
    }
 

    public void SetCanMove()
    {
        CanEntityMove = true;
    }
    public void SetCanNOTMove()
    {
        CanEntityMove = false;
    }
}
