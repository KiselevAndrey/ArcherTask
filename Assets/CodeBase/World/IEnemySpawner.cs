using CodeBase.Character.Enemy;
using CodeBase.Character.Player;
using UnityEngine;

namespace CodeBase.World
{
    public interface IEnemySpawner
    {
        public IEnemy NearbyEnemy { get; }

        public void FindNearestEnemy(Vector3 startPosition);
        public void SetPlayer(IPlayer player);
    }
}