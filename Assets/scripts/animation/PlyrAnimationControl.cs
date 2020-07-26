using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlyrAnimationControl : MonoBehaviour, IAnimate
{
    private Animator Anim;
    private character _character;

    private Collider[] AttackResults;
    private float LightattackRadius = 0.5f;

    private float LightattackOffset = 1;
    private float lerpTargetX;
    private float lerpTargetZ;

    #region animHashIDs
    private int speedParam;
    private int punchParam;
    private int turnRightParam;
    private int turnLeftParam;
    private int EnemiesNearParam;
    private int FightModeVelXParam;
    private int FightModeVelZParam;
    private int TakeHitParam;
    private int Deathparam;
    private int MediumPunchParam;
    private int HeavyPunchParam;
    private int KickParam;
    private int MediumKickParam;
    private int HeavyKickParam;
    private int PowerKickParam;
    private int PowerPunchParam;
    private int DodgeParam;
    private int DodgeVelXParam;
    private int DodgeVelZParam;
    #endregion

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
        _character = GetComponentInParent<character>();

        SetAnimHashIDs();

    }
    private void OnEnable()
    {
        _character.OnDied += DeathAnim;
    }
    private void OnDisable()
    {
        _character.OnDied -= DeathAnim;
    }

    public void ControlLocomotionStepping(bool ent = false)
    {
        Vector3 dir = transform.TransformDirection(_character.Controller.getDirection().normalized);//
        Vector3 dirS = transform.InverseTransformDirection(_character.Controller.getDirection().normalized);
        lerpTargetX = Mathf.Lerp(lerpTargetX, dir.x,  0.1f);
        lerpTargetZ = Mathf.Lerp(lerpTargetZ, dirS.z, 0.1f);
        SetLerpParams();

    }

    public void ControlIdleAnimParams(bool val)
    {
        Anim.SetBool(EnemiesNearParam, val);
    }

    public void SetAnimPunchAttackTrigger(int ComboIndex)
    {
        switch (ComboIndex)
        {
            case 0:
                Anim.SetTrigger(punchParam);
                break;
            case 1:
                Anim.SetTrigger(MediumPunchParam);
                break;
            case 2:
                Anim.SetTrigger(HeavyPunchParam);
                break;
        }

    }
    public void SetLerpTrigger()
    {
        SetLerpParams();
        Anim.SetTrigger(DodgeParam);
    }
    void SetLerpParams()
    {
        Anim.SetFloat(FightModeVelXParam, lerpTargetX * 1.5f);
        Anim.SetFloat(FightModeVelZParam, lerpTargetZ * 1.5f);
    }

    public void SetPowerKickAttack()
    {
        Anim.SetTrigger(PowerKickParam);
    }
    public void SetAnimKickAttackTrigger(int ComboIndex)
    {
        switch (ComboIndex)
        {
            case 0:
                Anim.SetTrigger(KickParam);
                break;
            case 1:
                Anim.SetTrigger(MediumKickParam);
                break;
            case 2:
                Anim.SetTrigger(HeavyKickParam);
                break;
        }

    }
    public void SetWalkRunParameter(float LerpParam)
    {
        Anim.SetBool("isMoving", _character.EntityMover.isEntityMoving());
        float curSpeed = Anim.GetFloat(speedParam);
        float magnitude = new Vector3 (_character.Controller.Horizontal(), 0,_character.Controller.Vertical()).magnitude;
 
        if (_character.EntityMover.isEntityMoving())
        {
            Anim.SetFloat(speedParam, magnitude);
        }
        else 
        {
            Anim.SetFloat(speedParam, Mathf.Lerp(curSpeed, 0, LerpParam / 2));
        }
        _character.EntityMover.moveSpeed = curSpeed * 3f;
    }

    public void SetHitTrigger(int AttackDamage)
    {
        Anim.SetTrigger(TakeHitParam);
    }
    public void DeathAnim(IDie entityUnused)
    {
        Anim.SetTrigger(Deathparam);
    }

    public void SetAnimHashIDs()
    {
        speedParam = Animator.StringToHash("Speed");
        punchParam = Animator.StringToHash("Punch");
        MediumPunchParam = Animator.StringToHash("MedPunch");
        HeavyPunchParam = Animator.StringToHash("HeavyPunch");
        EnemiesNearParam = Animator.StringToHash("EnemiesNear");
        FightModeVelXParam = Animator.StringToHash("FightMoveVelX");
        FightModeVelZParam = Animator.StringToHash("FightMoveVelZ");
        TakeHitParam = Animator.StringToHash("TakeHit");
        Deathparam = Animator.StringToHash("Death");
        KickParam = Animator.StringToHash("Kick");
        MediumKickParam = Animator.StringToHash("MedKick");
        HeavyKickParam = Animator.StringToHash("HeavyKick");
        PowerKickParam = Animator.StringToHash("PowerKick");
        PowerPunchParam = Animator.StringToHash("PowerPunch");
        DodgeParam = Animator.StringToHash("Dodge");
    }

    public void SetPowerPunchAttack()
    {
        Anim.SetTrigger(PowerPunchParam);
    }

}
