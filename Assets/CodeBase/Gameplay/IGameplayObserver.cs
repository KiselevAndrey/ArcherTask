using System;

namespace CodeBase.Gameplay
{
    public interface IGameplayObserver
    {
        public event Action OnFail;
        public void PrepareGame();
    }
}