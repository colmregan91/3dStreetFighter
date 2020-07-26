using UnityEngine;

public class ProjectileAttacker : IAttack
{
    public Transform entityTransform { get; private set; }
    public PooledMonoBehavior Projectile { get; private set; }

    private IAnimate animator;
    private int bulletCount;
    public int AttackDamage { get { return 5; } }
    Attacker _attacker;

    public ProjectileAttacker(Attacker attacker, Transform Entitytransform, PooledMonoBehavior projectile)
    {
        _attacker = attacker;
        entityTransform = Entitytransform;
        Projectile = projectile;
        animator = Entitytransform.GetComponentInChildren<IAnimate>();
        bulletCount = 5;
    }

    public void Attack(int unused,int unused2)
    {
        if (CanAttack())
        {
           
            Projectile.Get<projectile>(entityTransform.position + Vector3.up, entityTransform.rotation);
            bulletCount--;
            if (bulletCount <= 0)
            {
                _attacker.SetCurrentAttack(_attacker.punchHandler);
            }
        }

    }

    public bool CanAttack()
    {
        return true;
    }
}
