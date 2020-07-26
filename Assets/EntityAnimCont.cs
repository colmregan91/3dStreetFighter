using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAnimCont : MonoBehaviour
{
    private Animator Anim;
    private character _character;

    private int speedParam;
    private int punchParam;
    private int turnParam;

    private Collider[] AttackResults;
    private float LightattackRadius = 0.5f;

    private float LightattackOffset = 1;


    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
        _character = GetComponentInParent<character>();
        AttackResults = new Collider[5];
        speedParam = Animator.StringToHash("Speed");
        punchParam = Animator.StringToHash("Punch");
        turnParam = Animator.StringToHash("Turn");
    }
    //public void SetTurnIdle(bool val)
    //{
    //    Anim.SetBool(turnParam, val);
    //}
}
