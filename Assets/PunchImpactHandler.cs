using UnityEngine;

public class PunchImpactHandler : IAttack, IPowerAttack
{
    private Collider[] AttackResults;
    public Vector3 offsetSpherePos;
    public float offsetSphereRadius = 1;
    private IAnimate Ianimator;
    private int CurrentAttackDamage;
    public Transform entityTransform { get; private set; }
    public int AttackDamage { get { return CurrentAttackDamage; } }

    public PunchImpactHandler(Transform Entitytransform)
    {
        entityTransform = Entitytransform;
        AttackResults = new Collider[1];
        Ianimator = Entitytransform.GetComponentInChildren<IAnimate>();
    }


    public void HandleAttackImpact(Vector3 Offsetpos, float Offsetradius, int damage)
    {
        Vector3 position = entityTransform.position + entityTransform.forward + Offsetpos;
        int results = Physics.OverlapSphereNonAlloc(position, Offsetradius, AttackResults);

        for (int i = 0; i < results; i++)
        {
         
               var takeHitObj = AttackResults[i].GetComponent<ITakeHits>();
            if (takeHitObj != null)
            {
                CurrentAttackDamage = damage;
                takeHitObj.TakeHit(this);
            }
        }
    }

    public void PowerPunchAttack()
    {
        Ianimator.SetPowerPunchAttack();
    }
    public void PowerKickAttack()
    {
        Ianimator.SetPowerKickAttack();
    }

    public void Attack(int ComboIndex,int attackStyleIndex)
    {
        if (!CanAttack()) return;

        switch (attackStyleIndex)
        {
            case 0: Ianimator.SetAnimPunchAttackTrigger(ComboIndex);
                break;
            case 1: Ianimator.SetAnimKickAttackTrigger(ComboIndex);
                break;
        }
    }

    public bool CanAttack()
    {
        return true;
    }
}
