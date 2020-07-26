using UnityEngine;
using UnityEngine.AI;

public class ReadyToFight : Istate
{
    NavMeshAgent _agent;
    Animator _anim;
    private character _target;
    bool Turn;
    private float AttackTimer;
    private float attackRefreshRate = 1;
    private enemy thisenemy;
    public ReadyToFight(NavMeshAgent agent, Animator anim, character Target)
    {
        _agent = agent;
        _anim = anim;
        _target = Target;
        thisenemy = agent.GetComponentInParent<enemy>();
    }
    public void OnEnter()
    {
        AttackTimer = 0;
        _anim.SetBool("ReadyToFight", true);
        _agent.isStopped = true;
    }

    public void ONExit()
    {
        _anim.SetBool("ReadyToFight", false);
        _agent.isStopped = false;
    }

    public void OnTick()
    {
        Quaternion g = Quaternion.LookRotation((_target.transform.position + new Vector3(0, 0 - 2)) - _agent.transform.position);
        g.x = 0;
        g.z = 0;
        _agent.transform.rotation = Quaternion.Slerp(_agent.transform.rotation, g, 1f);

        AttackTimer += Time.deltaTime;
        if (AttackTimer >= attackRefreshRate)
        {
            AttackTimer = 0;
            thisenemy.Attack();
        }
    }

}