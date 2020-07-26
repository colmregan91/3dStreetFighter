using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class box : MonoBehaviour, ITakeHits
{
    public float hitForce;
    private Rigidbody Rb;

    public bool Alive { get { return true; } }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
    }
    public void TakeHit (IDamage hitBy)
    {
        var dir = Vector3.Normalize (transform.position - hitBy.entityTransform.position);
        Rb.AddForce(dir * hitBy.AttackDamage * 2, ForceMode.Impulse);
    }
}
