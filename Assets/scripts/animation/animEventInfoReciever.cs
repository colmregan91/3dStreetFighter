using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animEventInfoReciever : MonoBehaviour
{
    public Attacker attacker { get; private set; }
    private IEntity entity;
    private IMove Mover;
    private ILerp dodgeCont;
    public Transform hand;
    public Transform foot;
    public HeavyAttackParticles PunchThirdComboParticle;
    public HeavyAttackParticles KickThirdComboParticle;


    private character _character;
    private void Start()
    {
        entity = GetComponentInParent<IEntity>();
        dodgeCont = entity.LerpCont;
        Mover = entity.EntityMover;

        attacker = GetComponentInParent<Attacker>();
    }

    // Anim event
    public void AttackImpactInfoReciever(int damage)
    {
        attacker.HandleHitImpact(damage);
    }
    public void setEntityKnockedDown()
    {
        entity.isKnocked = true;
        Mover.SetCanNOTMove();
    }
    public void setEntityRecovered()
    {
        entity.isKnocked = false;
        Mover.SetCanMove();
    }
    // Anim events
    public void SetCanEntityMove()
    {
        entity.EntityRotation.shouldRotate = true;
        Mover.SetCanMove();
    }
    public void SetWindingDownTrue()
    {
        attacker.windingDown = true;
    }
    public void SetWindingDownFalse()
    {
        attacker.windingDown = false;
    }
    public void SetCanEntityNOTMove()
    {
        entity.EntityRotation.shouldRotate = false;
        Mover.SetCanNOTMove();
    }

    public void GetPunchComboParticle()
    {
        var particle = PunchThirdComboParticle.Get<HeavyAttackParticles>(hand.position, Quaternion.identity);
        particle.posTarget = hand;
    }
    public void GetKickComboParticle()
    {
        var particle = KickThirdComboParticle.Get<HeavyAttackParticles>(foot.position, Quaternion.identity);
        particle.posTarget = foot;
    }
    public void ResetDodging()
    {
        dodgeCont.ResetLerp();
    }
}
