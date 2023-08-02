using System;

namespace CodeBase.Character.Player.State
{
    internal class DieState : ICharacterState
    {
        private readonly Action _onFail;

        public DieState(Action onFail)
        {
            _onFail = onFail;
        }

        public void Enter()
        {
            _onFail?.Invoke();
        }

        public void Exit() { }

        public void Update() { }
    }
}