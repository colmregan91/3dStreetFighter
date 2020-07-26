using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class character : PooledMonoBehavior, ITakeHits, IDie, IEntity
{
    public event Action<int, int> OnHealthChanged;
    public event Action<IDie> OnDied;

    [SerializeField] private PooledMonoBehavior hitImpact;
    [SerializeField] private PooledMonoBehavior DeathImpact;

    public IControlPlayers Controller { get; private set; }
    public IMove EntityMover { get; private set; }
    public IRotate EntityRotation { get; private set; }
    public IAnimate EntityanimCont { get; private set; }
    public ILerp LerpCont { get; private set; }

    public List<enemy> enemiesWithinFightingDistance = new List<enemy>();

    public int maxHealth;
    public int currentHealth;
    public bool inFightingDistance { get { return EntityTarget != null && EntityTarget.inFightingDistance; } }

    public bool Alive { get; private set; }

    private Attacker attacker;

    private int KickingAttackIndex = 1;
    private int PunchingAttackIndex = 0;
    public float KickChargeStartTime;
    public float PunchChargeStartTime;

    public enemy EntityTarget;
    public IEntity Target { get { return EntityTarget; } }

    public bool isKnocked { get; set; }

    public void AddEnemyToFightingDistanceList(enemy enemy)
    {
        enemiesWithinFightingDistance.Add(enemy);
    }
    public void RemoveEnemyToFightingDistanceList(enemy enemy)
    {
        enemiesWithinFightingDistance.Remove(enemy);
    }
    private void OnEnable()
    {
        Alive = true;
        currentHealth = maxHealth;

        if (!CharacterManager.allCharacters.Contains(this))
        {
            CharacterManager.allCharacters.Add(this);
        }
    }

    protected override void OnDisable()
    {
        if (CharacterManager.allCharacters.Contains(this))
        {
            CharacterManager.allCharacters.Remove(this);
        }

        base.OnDisable();
    }
    private void Awake()
    {
        attacker = GetComponent<Attacker>();
        EntityanimCont = GetComponentInChildren<PlyrAnimationControl>();

    }
    private void Start()
    {
        EntityRotation = new PlayerRotation(transform, Controller);
        EntityMover = new PlayerMover(Controller, transform);
        LerpCont = new DodgeController(this, Controller);
        EntityMover.SetCanMove();
    }
    public void SetController(IControlPlayers controller)
    {
        Controller = controller;
    }
    private void Update()
    {
     

        if (Input.GetKeyDown(KeyCode.O))
        {
            attacker.SetCurrentAttack(attacker.projectileAttacker);
        }

        EntityTarget = CharacterManager.GetClosestEnemyToCharacter(this);
 
    }

    public void GetActionInput()
    {
        if (LerpCont.isLerping) return;

        if (Controller.Circle_Button())
        {
            attacker.InvokeCurrentAttack(PunchingAttackIndex);
        }
        if (Controller.X_Button())
        {
            attacker.InvokeCurrentAttack(KickingAttackIndex);
        }


        if (Controller.Hold_X_Button())
        {
            KickChargeStartTime += Time.deltaTime;
            if (KickChargeStartTime >= 0.8f)
            {
                attacker.InvokePowerfulKick();
                KickChargeStartTime = 0;
            }
        }
        else
        {
            KickChargeStartTime = 0;
        }


        if (Controller.Hold_Circle_Button())
        {
            PunchChargeStartTime += Time.deltaTime;
            if (PunchChargeStartTime >= 1)
            {
                attacker.InvokePowerfulPunch();
                PunchChargeStartTime = 0;
            }
        }
        else
        {
            PunchChargeStartTime = 0;
        }

 
    }

    public void TakeHit(IDamage hitBy)
    {
        if (!Alive || isKnocked) return;

        currentHealth -= hitBy.AttackDamage;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            TakeNonLethalHit(hitBy);
        }
    }
    private void TakeNonLethalHit(IDamage hitBy)
    {
        EntityanimCont.SetHitTrigger(hitBy.AttackDamage);
        hitImpact.Get<Particles>(transform.position + Vector3.up, Quaternion.identity);
        OnHealthChanged(currentHealth, maxHealth);
    }
    private void Die()
    {
        Alive = false;
        OnDied?.Invoke(this);
    }
}

public interface IEntity
{
    Transform transform { get; }
    IMove EntityMover { get; }
    IRotate EntityRotation { get; }
    IAnimate EntityanimCont { get; }
    ILerp LerpCont { get; }

    IEntity Target { get; }
    bool inFightingDistance { get; }
    bool isKnocked { get; set; }
}
