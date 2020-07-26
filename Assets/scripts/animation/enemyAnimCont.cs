using UnityEngine;

public class enemyAnimCont : MonoBehaviour, IAnimate
{
    private ILerp lerper;
    private Animator Anim;
    private int inFightingDistanceParam;
    private int FightReadyParam;
    private int HitParam;
    private int medHitParam;
    private int bigHitParam;
    private int PowerKickHitParam;
    private int KnockOutParam;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        SetAnimHashIDs();
    }
    private void Start()
    {
        lerper = GetComponentInParent<enemy>().LerpCont;
    }
    public void ControlIdleAnimParams(bool val)
    {
        Anim.SetBool(inFightingDistanceParam, val);
    }

    public void ControlLocomotionStepping(bool ent = false)
    {
        Debug.Log("EnemyLoco");
        Anim.SetBool(FightReadyParam, ent);
    }

    public void DeathAnim(IDie entityUnused)
    {
        Anim.SetTrigger(KnockOutParam);
    }

    public void SetAnimHashIDs()
    {
        inFightingDistanceParam = Animator.StringToHash("inFightingDistance");
        FightReadyParam = Animator.StringToHash("FightReady");
        HitParam = Animator.StringToHash("Hit");
        medHitParam = Animator.StringToHash("MedHit");
        bigHitParam = Animator.StringToHash("BigHit");
        PowerKickHitParam = Animator.StringToHash("PowerKickHit");
        KnockOutParam = Animator.StringToHash("KnockOut");
    }

    public void SetAnimKickAttackTrigger(int comboIndex)
    {
    }

    public void SetAnimPunchAttackTrigger(int ComboIndex)
    {
    }

    public void SetWalkRunParameter(float LerpParam)
    {

    }

    public void SetLerpTrigger()
    {
        Anim.SetTrigger(PowerKickHitParam);
    }

    public void SetHitTrigger(int AttackDamage)
    {
        switch (AttackDamage)
        {
            case 1:
                Anim.SetTrigger(HitParam);
                break;
            case 3:
                Anim.SetTrigger(medHitParam);
                break;
            case 5:
                Anim.SetTrigger(bigHitParam);
                break;
            case 6:
                lerper.SetLerp();
                break;
        }

    }

    public void SetPowerKickAttack()
    {
    }

    public void SetPowerPunchAttack()
    {
    }
}