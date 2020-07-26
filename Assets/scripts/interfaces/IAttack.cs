using UnityEngine;

public interface IAttack : IDamage
{   
    bool CanAttack();

    void Attack(int ComboIndex, int attackStyleIndex);
}

public interface IDamage
{
    Transform entityTransform { get; }
    int AttackDamage { get; }
}

public interface IPowerAttack : IAttack
{
    void PowerKickAttack();
    void PowerPunchAttack();
}