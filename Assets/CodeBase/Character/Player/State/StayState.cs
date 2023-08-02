using CodeBase.Character.Enemy;
using CodeBase.World;

namespace CodeBase.Character.Player.State
{
    public class StayState : ICharacterState
    {
        private readonly IEnemySpawner _enemySpawner;
        private readonly PlayerMover _mover;

        private IEnemy _nearestEnemy;

        public StayState(PlayerMover mover, IEnemySpawner enemySpawner)
        {
            _mover = mover;
            _enemySpawner = enemySpawner;
        }

        public void Enter() =>
            FindNearestEnemy();

        public void Exit() { }

        public void Update()
        {
            if (_nearestEnemy != null)
                _mover.LookAt(_nearestEnemy.Transform.position);
            else
                FindNearestEnemy();
        }

        private void FindNearestEnemy()
        {
            _enemySpawner.FindNearestEnemy(_mover.Transform.position);
            _nearestEnemy = _enemySpawner.NearbyEnemy;
        }
    }
}