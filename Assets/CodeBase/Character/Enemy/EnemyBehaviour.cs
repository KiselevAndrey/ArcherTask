using CodeBase.Character.Player;
using CodeBase.Infrastructure;
using CodeBase.World;
using System;
using UnityEngine;

namespace CodeBase.Character.Enemy
{
    public class EnemyBehaviour : CharacterBehaviour, ISpawnedEnemy, ICoroutineRunner
    {
        [Header("Enemy parameters")]
        [SerializeField] private Vector2Int _waitTime;
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private EnemyDamagingService _attacker;

        private readonly PlayerFinder _playerFinder = new();

        private Action<IEnemy> _despawn;

        public void Init(Map map, Vector3 spawnPosition, Func<IPlayer> getPlayer, Action<IEnemy> despawn)
        {
            transform.position = spawnPosition;
            _playerFinder.Init(getPlayer);
            _mover.Init(map, transform, _playerFinder);
            _despawn = despawn;
        }

        protected override void OnAwake()
        {
            _stateMachine = new State.EnemyStateMachine(_mover, _waitTime, this, _attacker, _playerFinder, Die);
            _attacker.Init(this);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out IPlayer player))
                player.TakeDamage(_attacker.BodyDamage());
        }

        private void Die() =>
            _despawn?.Invoke(this);
    }
}