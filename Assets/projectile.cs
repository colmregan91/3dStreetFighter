using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : PooledMonoBehavior, IDamage
{
    public float moveSpeed;
    public PooledMonoBehavior ImpactParticle;
    public PooledMonoBehavior shootParticle;
    public Transform entityTransform { get { return transform; } }

    public int AttackDamage { get { return 1; } }

    private void Start()
    {
        shootParticle.Get<Particles>(transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject.GetComponent<ITakeHits>();
        if (hit != null)
        {
            Impact(hit);
            ReturnToPool();
        }
        else
        {
            ImpactParticle.Get<Particles>(transform.position, Quaternion.identity);
            ReturnToPool();
        }
    }

    private void Impact(ITakeHits hit)
    {
        ImpactParticle.Get<Particles>(transform.position, Quaternion.identity);
        hit.TakeHit(this);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}
