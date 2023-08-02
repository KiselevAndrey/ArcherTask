using System;

namespace CodeBase.Character.Enemy.State
{
    internal class DieState : ICharacterState
    {
        private readonly EnemyMover _mover;
        private readonly Action _die;

        public DieState(EnemyMover mover, Action die)
        {
            _mover = mover;
            _die = die;
        }

        public void Enter()
        {
            _mover.Stop();
            _die?.Invoke();
        }

        public void Exit() { }

        public void Update() { }
    }
}