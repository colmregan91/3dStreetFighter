using UnityEngine;

public class HeavyAttackParticles : PooledMonoBehavior
{
    public Transform posTarget;
    private void Update()
    {
        if (posTarget == null) return;

        transform.position = posTarget.position;
    }

}
