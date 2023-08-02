using CodeBase.Character.Player.State;
using CodeBase.Gameplay;
using CodeBase.World;
using System;
using UnityEngine;

namespace CodeBase.Character.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
    public class PlayerBehaviour : CharacterBehaviour, IPlayer, IGameplayObserver
    {
        [Header("External classes")]
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private EnemySpawner _enemySpawner;

        [Header("Internal classes")]
        [SerializeField] private PlayerMover _mover;
        [SerializeField] private PlayerDamagingService _attacker;

        private Rigidbody _rigidbody;
        private Vector3 _startPosition;

        public Vector2 InputVector => _joystick.Direction;

        public event Action OnFail;

        public void PrepareGame()
        {
            transform.position = _startPosition;
        }

        protected override void OnAwake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPosition = transform.position;

            _stateMachine = new PlayerStateMachine(this, _mover, _attacker, _enemySpawner, OnFail);
            _mover.Init(_rigidbody, transform);
            _attacker.Init();
            _enemySpawner.SetPlayer(this);
        }

        protected override void OnFixedUpdate()
        {
            if (InputVector != Vector2.zero)
                _stateMachine.ChangeState(CharactersState.Move);
            else
                _stateMachine.ChangeState(_attacker.CanAttack()
                    ? CharactersState.Attack
                    : CharactersState.Stay);

            base.OnFixedUpdate();
        }
    }
}