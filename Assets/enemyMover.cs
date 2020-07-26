using UnityEngine;
using UnityEngine.AI;

public class enemyMover : IMove
{
    private Transform EntityTransform;
    private enemy thisEnemy;
    private NavMeshAgent _agent;
    private float Movespeed = 1.5f;
    private float fightmoveSpeed = 0.5f;
    private bool CanMove;
    public enemyMover(Transform entityTrans, enemy enemy, NavMeshAgent agent)
    {
        EntityTransform = entityTrans;
        thisEnemy = enemy;
        _agent = agent;
    }
    public Transform entityTransform { get { return EntityTransform; } }
    public float moveSpeed { get { return Movespeed; } set { Movespeed = value; } }
    public float fightMoveSpeed { get { return fightmoveSpeed; } }
    public Vector3 getDirection { get { return new Vector3(0, 0, 1); } }
    public bool CanEntityMove { get { return CanMove; } set { CanMove = value; } }

    public bool isEntityMoving()
    {
        return true;
    }

    public void MoveTick(bool infightingDistance)
    {
        if (CanEntityMove)
        {
            _agent.speed = infightingDistance ? fightMoveSpeed : Movespeed;
            thisEnemy.EntityTarget = CharacterManager.GetClosestCharacterToEnemy(thisEnemy);
            _agent.SetDestination(thisEnemy.Target.transform.position);
            _agent.acceleration = (_agent.remainingDistance < _agent.stoppingDistance) ? 60 : 1f;
        }

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
