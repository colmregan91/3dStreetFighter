
public interface IAnimate
{
    void DeathAnim(IDie entityUnused);
    void SetHitTrigger(int AttackDamage);
    void SetWalkRunParameter(float LerpParam);
    void SetAnimPunchAttackTrigger(int ComboIndex);
    void SetAnimHashIDs();
    void ControlIdleAnimParams(bool val);
    void ControlLocomotionStepping(bool ent = false);
    void SetAnimKickAttackTrigger(int comboIndex);
    void SetPowerKickAttack();
    void SetPowerPunchAttack();
    void SetLerpTrigger();
}
