using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using System;

public class enemy : PooledMonoBehavior, ITakeHits, IDie, IEntity
{
    public event Action<IDie> OnDied;
    public event Action<int, int> OnHealthChanged;
    [SerializeField] private PooledMonoBehavior KnockOutParticle;
    [SerializeField] private PooledMonoBehavior HitImpactParticle;

    public Transform Chin;
    public int CurrentHealth;
    [SerializeField] private int maxHealth;

    private NavMeshAgent navMeshAgent;
    public float distance;
    public bool fightReady { get { return  distance <= (navMeshAgent.stoppingDistance + 0.1f); } }
    public bool inFightingDistance { get { return distance <= 3f; } }
    public int AttackDamage { get { return 1; } }

    public bool isKnocked { get; set; }
    public bool Alive { get; private set; }

    public IMove EntityMover { get; private set; }
    public IRotate EntityRotation { get; private set; }
    public IAnimate EntityanimCont { get; private set; }
    public ILerp LerpCont { get; private set; }

    public character EntityTarget;
    public IEntity Target { get { return EntityTarget; } }

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }
    void Awake()
    {
        EntityanimCont = GetComponentInChildren<enemyAnimCont>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        EntityMover = new enemyMover(transform, this, navMeshAgent);
        EntityRotation = new enemyRotator(transform);
        EntityRotation.shouldRotate = true;
        LerpCont = new enemyImpactMover(this);
        EntityMover.SetCanMove();
        Alive = true;
    }


    public void TakeHit(IDamage hitBy) 
    {
        if (!Alive || isKnocked) return;

        CurrentHealth -= hitBy.AttackDamage;

        if (CurrentHealth <= 0)
        {
            Die();
        }
        else
        {
            TakeNonLethalHit(hitBy.AttackDamage);
        }
    }

   public void ControlCharactersEnemiesList(bool inFightingDistance)
    {
        if (inFightingDistance)
        {
            if (!EntityTarget.enemiesWithinFightingDistance.Contains(this))
            {
                EntityTarget.AddEnemyToFightingDistanceList(this);

            }
        }
        else
        {

            if (EntityTarget.enemiesWithinFightingDistance.Contains(this))
            {
                EntityTarget.RemoveEnemyToFightingDistanceList(this);
            }
        }
    }

    public void Attack()
    {
        navMeshAgent.isStopped = true;
        //    anim.SetTrigger("Punch");
    }
    void TakeNonLethalHit(int AttackDamage)
    {
        EntityanimCont.SetHitTrigger(AttackDamage);

        HitImpactParticle.Get<Particles>(Chin.position + Vector3.up, Quaternion.identity);
    }
    void Die()
    {
        Alive = false;
        EntityTarget.RemoveEnemyToFightingDistanceList(this);
        GetComponent<Collider>().enabled = false;
        EntityanimCont.DeathAnim(this);
        KnockOutParticle.Get<Particles>(Chin.position, Quaternion.identity);
       // ReturnToPool(6f);
    }
    //private void OnDrawGizmos() // to View overlapSphere
    //{
    //    Vector3 pos = transform.position + transform.forward + GetComponentInChildren<animEventInfoReciever>().offsetSpherePos;
    //    Gizmos.DrawSphere(pos, GetComponentInChildren<animEventInfoReciever>().offsetSphereRadius);
    //}
}
