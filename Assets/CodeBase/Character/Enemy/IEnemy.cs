using CodeBase.World;
using System;
using UnityEngine;

namespace CodeBase.Character.Enemy
{
    public interface IEnemy : ICharacter
    {
    }

    public interface ISpawnedEnemy : IEnemy
    {
        public void Init(Map map, Vector3 spawnPosition, Func<Player.IPlayer> getPlayer, Action<IEnemy> despawn);
    }
}