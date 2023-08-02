using CodeBase.World;
using Pathfinding;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Character.Enemy
{
    [Serializable]
    public class EnemyMover : CharacterMover, IEnemyMover
    {
        [SerializeField, Min(0)] private float _speed;
        [field: SerializeField, Min(1)] public float MaxMoveDistance { get; private set; }
        [SerializeField, Range(0, 1)] private float _chanceMoveToPlayer;
        [SerializeField] private Vector2 _distanceFromPlayer;
        [SerializeField, Range(0, 1)] private float _chanceUseSprint;
        [SerializeField, Min(0)] private float _sprintSpeed;

        [SerializeField] AIPath _aiPath;
        [SerializeField] AIDestinationSetter _aiDestSetter;

        private Map _map;
        private PlayerFinder _playerFinder;

        public void Init(Map map, Transform transform, PlayerFinder playerFinder)
        {
            _map = map;
            Transform = transform;
            _playerFinder = playerFinder;

            _aiPath.maxSpeed = _speed;
            _aiDestSetter.enabled = false;
        }

        public void MoveToNewPoint()
        {
            _aiDestSetter.enabled = true;

            if (Random.value < _chanceMoveToPlayer)
                MoveToPlayer();
            else
                MoveToRandomPathPoint();

            _aiPath.maxSpeed = Random.value < _chanceUseSprint
                ? _sprintSpeed
                : _speed;

            _aiPath.SearchPath();
        }

        public void Stop()
        {
            _aiDestSetter.enabled = false;
            _aiPath.maxSpeed = 0;
        }

        public void LookAtPlayer()
        {
            var player = _playerFinder.GetPlayer();
            if (player != null) 
                LookAt(player.Transform.position);
        }

        private Vector3 FindPositionNearPlayer()
        {
            var player = _playerFinder.GetPlayer();
            if(player != null)
                return RandomPosition.InRing(player.Transform.position, _distanceFromPlayer);
            
            return GetRandomPosition();
        }

        private Vector3 GetRandomPosition() =>
            _map.GetRandomPoint();

        private void MoveToPlayer()
        {
            var player = _playerFinder.GetPlayer();
            if (player != null) {
                _aiPath.endReachedDistance = Random.Range(_distanceFromPlayer.x, _distanceFromPlayer.y);
                _aiDestSetter.target = player.Transform;
            }
            else
                MoveToRandomPathPoint();
        }

        private void MoveToRandomPathPoint()
        {
            _aiDestSetter.target = null;
            _aiPath.endReachedDistance = 0;

            Vector3 targetPosition = _map.GetRandomPoint();
            targetPosition.y = Transform.position.y;
            _aiDestSetter.TargetPosition = targetPosition;
        }
    }
}