using UnityEngine;

namespace CodeBase.Character.Enemy.State
{
    public class MoveState : ICharacterState
    {
        private readonly EnemyStateMachine _stateMachine;
        private readonly EnemyMover _mover;

        private Vector3 _lastPosition;
        private float _moveDistance;

        public MoveState(EnemyStateMachine stateMachine, EnemyMover mover)
        {
            _stateMachine = stateMachine;
            _mover = mover;
        }

        public void Enter()
        {
            _moveDistance = 0;
            _lastPosition = _mover.Transform.position - new Vector3(0, 0, 0.1f);
            _mover.MoveToNewPoint();
        }

        public void Exit() { }

        public void Update()
        {
            var position = _mover.Transform.position;
            if (position != _lastPosition
                && _moveDistance < _mover.MaxMoveDistance)
            {
                _moveDistance += Vector3.Distance(_lastPosition, position);
                _lastPosition = position;
            }
            else
            {
                _mover.Stop();
                _stateMachine.ChangeState(CharactersState.Stay);
            }
        }
    }
}