using CodeBase.Infrastructure;
using System.Collections;
using UnityEngine;

namespace CodeBase.Character.Enemy.State
{
    public class StayState : ICharacterState
    {
        private readonly Vector2 _waitTime;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly EnemyStateMachine _stateMachine;
        private readonly EnemyMover _mover;

        private Coroutine _waitCoroutine;

        public StayState(EnemyStateMachine enemyStateMachine, Vector2 waitTime, ICoroutineRunner coroutineRunner, EnemyMover mover)
        {
            _waitTime = waitTime;
            _coroutineRunner = coroutineRunner;
            _stateMachine = enemyStateMachine;
            _mover = mover;
        }

        public void Enter()
        {
            _waitCoroutine = _coroutineRunner.StartCoroutine(Wait());
        }

        public void Exit() 
        {
            if(_waitCoroutine != null ) 
                _coroutineRunner.StopCoroutine(_waitCoroutine);
        }

        public void Update() 
        {
            _mover.LookAtPlayer();
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(Random.Range(_waitTime.x, _waitTime.y));
            _stateMachine.ChangeState(CharactersState.Attack);
        }
    }
}