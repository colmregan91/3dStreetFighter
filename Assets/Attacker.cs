using System;
using System.Collections;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    private IAttack CurrentAttacker;
    private IPowerAttack PowerAttack;
    public PooledMonoBehavior Projectile;
    public float delay = 1f;
    private int ComboIndex;
    public PunchImpactHandler punchHandler;
    public ProjectileAttacker projectileAttacker;
    private float attackStartTime;
    public Vector3 offsetSpherePos;
    public float offsetSphereRadius = 0.55f;

    public bool attacking;
    public bool Powerattacking;
    public bool windingDown;

    private void Awake()
    {
        punchHandler = new PunchImpactHandler(transform);
        projectileAttacker = new ProjectileAttacker(this, transform, Projectile);
        SetCurrentAttack(punchHandler);
        SetPowerFulAttack(punchHandler);


    }
    private void OnDrawGizmos() // to View overlapSphere
    {
        Vector3 pos = transform.position + transform.forward + offsetSpherePos;
        Gizmos.DrawSphere(pos, offsetSphereRadius);
    }
    public void HandleHitImpact(int damage)
    {
        punchHandler.HandleAttackImpact(offsetSpherePos, offsetSphereRadius, damage);
    }
    public void SetCurrentAttack(IAttack attacker)
    {
           CurrentAttacker = attacker;
    }
    public void SetPowerFulAttack(IPowerAttack powerAttacker)
    {
        PowerAttack = powerAttacker;
    }

    public void InvokePowerfulKick()
    {
        if (!Powerattacking)
        {
            StartCoroutine("PowerAttackCheck");
            PowerAttack.PowerKickAttack();
        }
    }

    public void InvokeCurrentAttack(int AttackStyleIndex)
    {
        if (Powerattacking || windingDown) return;

        if (!attacking)
        {
            if (Time.time > attackStartTime + delay)
            {
                attackStartTime = Time.time;
                CurrentAttacker.Attack(ComboIndex, AttackStyleIndex);
                StartCoroutine("AttackInputCheck");
            }

        }
        else
        {
            StopCoroutine("AttackInputCheck");
            CurrentAttacker.Attack(ComboIndex, AttackStyleIndex);
            StartCoroutine("AttackInputCheck");
        }


    }
    private IEnumerator AttackInputCheck()
    {
        ComboIndex++;

        if (ComboIndex > 2)
        {
       //     windingDown = true;
           
        }
        attacking = true;
        yield return new WaitForSeconds(delay);
        attacking = false;
        ComboIndex = 0;
    }
    private IEnumerator PowerAttackCheck()
    {
        Powerattacking = true;
        yield return new WaitForSeconds(1.5f);
        Powerattacking = false;
    }

    internal void InvokePowerfulPunch()
    {
        if (!Powerattacking)
        {
            StartCoroutine("PowerAttackCheck");
            PowerAttack.PowerPunchAttack();
        }
    }
}
