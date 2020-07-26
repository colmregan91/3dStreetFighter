using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
public class EnemyStateMachine : MonoBehaviour
{
    private StateMachine enemyStateMachine;
    private NavMeshAgent navMeshAgent;

    private Moving _moving;
    private Lerping _lerping;
    private FightMode _fightMode;

    private enemy _thisEnemy;

    private IAnimate _enemyAnimControl;
    private void Awake()
    {
        _enemyAnimControl = GetComponentInChildren<IAnimate>();
        _thisEnemy = GetComponent<enemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        enemyStateMachine = new StateMachine();

        _moving = new Moving(_thisEnemy);
        _fightMode = new FightMode(_thisEnemy);
        _lerping = new Lerping(_thisEnemy);

        enemyStateMachine.AddTransition(_moving, _fightMode, () => _thisEnemy.inFightingDistance);
        enemyStateMachine.AddTransition(_fightMode, _moving, () => !_thisEnemy.inFightingDistance);

        enemyStateMachine.addTransitionFromAnyState(_lerping, () => _thisEnemy.LerpCont.isLerping);

        enemyStateMachine.AddTransition(_lerping, _moving, () => !_thisEnemy.LerpCont.isLerping && !_thisEnemy.inFightingDistance);
        enemyStateMachine.AddTransition(_lerping, _fightMode, () => !_thisEnemy.LerpCont.isLerping && _thisEnemy.inFightingDistance);


        enemyStateMachine.SetState(_moving);
    }
    void Update()
    {
        if (!_thisEnemy.Alive) return;

        _thisEnemy.EntityMover.MoveTick(_thisEnemy.inFightingDistance);

        _thisEnemy.ControlCharactersEnemiesList(_thisEnemy.inFightingDistance);

        _enemyAnimControl.ControlLocomotionStepping(_thisEnemy.fightReady);
        navMeshAgent.acceleration = (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance) ? 60 : 1f;

        _thisEnemy.EntityTarget = CharacterManager.GetClosestCharacterToEnemy(_thisEnemy);
        enemyStateMachine.Tick();

        
        _thisEnemy.distance = Vector3.Distance(transform.position, _thisEnemy.EntityTarget.transform.position);
    }

}
