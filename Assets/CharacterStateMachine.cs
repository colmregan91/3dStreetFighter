using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    private StateMachine _characterStateMachine;

    private Idle _idle;
    private Moving _moving;
    private character _Character;
    private Lerping _dodging;
    private FightMode _fightMode;

    private IAnimate _AnimationControl;

    private void Awake()
    {
        _Character = GetComponent<character>();
        _AnimationControl = GetComponentInChildren<IAnimate>();
    }
    private void Start()
    {
        _idle = new Idle(_Character);
        _moving = new Moving(_Character);
        _fightMode = new FightMode(_Character);
        _dodging = new Lerping(_Character);
        _characterStateMachine = new StateMachine();

        _characterStateMachine.AddTransition(_idle, _moving, () => _Character.EntityMover.isEntityMoving());
        _characterStateMachine.AddTransition(_moving, _idle, () => !_Character.EntityMover.isEntityMoving());
        _characterStateMachine.AddTransition(_idle, _fightMode, () => _Character.inFightingDistance);
        _characterStateMachine.AddTransition(_moving, _fightMode, () => _Character.inFightingDistance);

        _characterStateMachine.AddTransition(_fightMode, _dodging, () => _Character.LerpCont.isLerping);
        _characterStateMachine.AddTransition(_dodging, _fightMode, () => !_Character.LerpCont.isLerping && _Character.inFightingDistance);
        _characterStateMachine.AddTransition(_dodging, _moving, () => !_Character.LerpCont.isLerping && !_Character.inFightingDistance);


        _characterStateMachine.AddTransition(_fightMode, _moving, () => !_Character.inFightingDistance && _Character.EntityMover.isEntityMoving());
        _characterStateMachine.AddTransition(_fightMode, _idle, () => !_Character.inFightingDistance && !_Character.EntityMover.isEntityMoving());
        _characterStateMachine.SetState(_idle);

    }
    private void Update()
    {
        if (!_Character.Alive) return;

        _Character.EntityTarget = CharacterManager.GetClosestEnemyToCharacter(_Character);

        _Character.GetActionInput();

        _characterStateMachine.Tick();
    }
}

public class Lerping : Istate
{
    private Vector3 targetPos;
    private IEntity _entity;
    private ILerp _LerpCont;
    private float lerpStartTime;
    private float lerpDuration = 3f;
    public Lerping(IEntity entity)
    {
        _entity = entity;
        _LerpCont = _entity.LerpCont;
    }
    public void OnEnter()
    {
        if (_LerpCont is enemyImpactMover)
        {
            lerpDuration = 2;
            _entity.EntityRotation.RotationTick(_entity.Target);
        }
        else
        {
            lerpDuration = 3;
        }
        lerpStartTime = Time.time;
        targetPos = _LerpCont.GetTargetPos();
    }

    public void ONExit()
    {
        targetPos = Vector3.zero;
    }

    public void OnTick()
    {
        if (targetPos == Vector3.zero) return;

        if (_LerpCont is character)
        {
            if (Time.time < lerpStartTime + 0.2f)
            {
                return;
            }
        }
        float TImeSinceStarted = Time.time - lerpStartTime;
        float percent = (TImeSinceStarted / lerpDuration);
        _LerpCont.LerpTick(targetPos, percent);
    }
}

