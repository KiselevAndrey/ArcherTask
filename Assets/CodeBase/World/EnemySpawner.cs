using CodeBase.Character.Enemy;
using CodeBase.Character.Player;
using CodeBase.Gameplay;
using CodeBase.Options;
using CodeBase.Utility.Extension;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Lean.Pool.LeanPool;

namespace CodeBase.World
{
    public class EnemySpawner : MonoBehaviour, IEnemySpawner, IGameplayObserver
    {
        [field: SerializeField] private List<EnemyBehaviour> _enemyPrefabs;
        [SerializeField] private Transform _leftDownPoint;
        [SerializeField] private Transform _rightUpPoint;
        [SerializeField] private GameplayOptionsSO _optionsSO;
        [SerializeField, Range(0, 2)] private float _emptySpawnRadius;

        [Space]
        [SerializeField] private Map _map;

        private List<IEnemy> _enemies = new();
        private IPlayer _player;

        public event Action OnFail;
        public event Action OnEnemyDie;
        public IEnemy NearbyEnemy { get; private set; }

        public void SetPlayer(IPlayer player) =>
            _player = player;

        public void FindNearestEnemy(Vector3 startPosition)
        {
            NearbyEnemy = null;
            var nearestDistance = 100f;

            foreach (var enemy in _enemies)
            {
                var distance = Vector3.Distance(startPosition, enemy.Transform.position);
                if(distance < nearestDistance)
                {
                    NearbyEnemy = enemy;
                    nearestDistance = distance;
                }
            }
        }

        public void PrepareGame()
        {
            _enemies.Clear();
            DespawnAll();

            _enemies = new(_optionsSO.EnemyCount);
            SpawnAll();
        }

        private void OnDisable()
        {
            _enemies.Clear();
            DespawnAll();
        }

        private void SpawnAll()
        {
            Collider[] overlapResult = new Collider[32];
            while (_enemies.Count < _optionsSO.EnemyCount)
                SpawnWithPool(_enemyPrefabs.Random(), overlapResult);
        }

        private void SpawnWithPool(EnemyBehaviour prefab, Collider[] overlapResult)
        {
            var spawnPosition = Vector3.zero;
            var obstaclesInSpawn = 0;
            var iteration = 0;
            var maxIteration = 100;
            do
            {
                spawnPosition = RandomPosition.InCube(_leftDownPoint.position, _rightUpPoint.position);
                obstaclesInSpawn = Physics.OverlapSphereNonAlloc(spawnPosition, _emptySpawnRadius, overlapResult);
                iteration++;
            } while (obstaclesInSpawn > 0 && iteration < maxIteration);

            if(obstaclesInSpawn == 0)
            {
                var enemy = Spawn(prefab);
                enemy.Init(_map, spawnPosition, GetPlayer, DespawnWithPool);
                _enemies.Add(enemy);
            }
        }

        private IPlayer GetPlayer() => _player;

        private void DespawnWithPool(IEnemy enemy)
        {
            _enemies.Remove(enemy);
            Despawn(enemy.Transform.gameObject);

            OnEnemyDie?.Invoke();

            if (_enemies.Count == 0)
                OnFail?.Invoke();
        }
    }
}