using UnityEngine;

namespace CodeBase.Character.Enemy.State
{
    public class AttackState : ICharacterState
    {
        private readonly EnemyStateMachine _stateMachine;
        private readonly IEnemyDamagingService _attacker;
        private readonly EnemyMover _mover;
        private readonly PlayerFinder _playerFinder;

        public AttackState(EnemyStateMachine enemyStateMachine, IEnemyDamagingService attacker, EnemyMover mover, PlayerFinder playerFinder)
        {
            _stateMachine = enemyStateMachine;
            _attacker = attacker;
            _mover = mover;
            _playerFinder = playerFinder;
        }

        public void Enter()
        {
            var player = _playerFinder.GetPlayer();
            if (player == null)
            {
                _stateMachine.ChangeState(CharactersState.Move);
                return;
            }

            var playerPosition = player.Transform.position;
            if (_attacker.CanAttack(playerPosition))
            {
                _stateMachine.BlockChangeState(true);
                _mover.LookAtPlayer();
                _attacker.TryAttack(playerPosition);
            }
        }

        public void Exit() { }

        public void Update()
        {
            if (_attacker.AttackEnded)
            {
                _stateMachine.BlockChangeState(false);
                _stateMachine.ChangeState(CharactersState.Move);
            }
        }
    }
}