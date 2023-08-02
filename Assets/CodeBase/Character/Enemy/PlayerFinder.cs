using CodeBase.Character.Player;
using System;

namespace CodeBase.Character.Enemy
{
    public class PlayerFinder
    {
        private Func<IPlayer> _getPlayer;

        public void Init(Func<IPlayer> getPlayer)
        {
            _getPlayer = getPlayer;
        }

        public IPlayer GetPlayer() => _getPlayer?.Invoke();
    }
}