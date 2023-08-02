using CodeBase.World;

namespace CodeBase.Character.Player.State
{
    public class AttackState : ICharacterState
    {
        private readonly IStateMachineInternal _stateMachine;
        private readonly IDamagingService _attacker;
        private readonly PlayerMover _mover;
        private readonly IEnemySpawner _enemySpawner;

        public AttackState(IStateMachineInternal stateMachine, IDamagingService attacker, PlayerMover mover, IEnemySpawner enemySpawner)
        {
            _stateMachine = stateMachine;
            _attacker = attacker;
            _mover = mover;
            _enemySpawner = enemySpawner;
        }

        public void Enter()
        {
            _stateMachine.BlockChangeState(true);
            _enemySpawner.FindNearestEnemy(_mover.Transform.position);
            var enemy = _enemySpawner.NearbyEnemy;
            if (enemy != null)
            {
                _mover.LookAt(enemy.Transform.position);
                _attacker.TryAttack(enemy.Transform.position);
            }
            else
                _stateMachine.BlockChangeState(false);
        }

        public void Exit() { }

        public void Update()
        {
            _stateMachine.BlockChangeState(_attacker.AttackEnded == false);
        }
    }
}