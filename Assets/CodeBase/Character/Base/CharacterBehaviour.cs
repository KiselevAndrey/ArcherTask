using System;
using UnityEngine;

namespace CodeBase.Character
{
    public abstract class CharacterBehaviour : MonoBehaviour, ICharacter
    {
        [SerializeField] private CharacterDamageable _damageable;

        public Transform Transform => transform;
        public Action<int> OnHealthChanged;

        protected IStateMachine _stateMachine;

        public bool IsAlive =>
            _damageable.IsAlive;

        public void TakeDamage(int damage)
        {
            _damageable.TakeDamage(damage);

            if (IsAlive == false)
            {
                _stateMachine.ChangeState(CharactersState.Die);
                OnHealthChanged?.Invoke(0);
            }
            else
                OnHealthChanged?.Invoke(_damageable.Health);
        }

        protected abstract void OnAwake();
        protected virtual void OnStart() { }
        protected virtual void OnEnabled() { }
        protected virtual void OnFixedUpdate() =>
            _stateMachine.UpdateState();

        private void Awake()
        {
            OnAwake();
        }

        private void Start() =>
            OnStart();

        private void OnEnable()
        {
            _damageable.Init();
            OnHealthChanged?.Invoke(_damageable.Health);
            _stateMachine.ChangeState(CharactersState.Stay);

            OnEnabled();
        }

        private void FixedUpdate() =>
            OnFixedUpdate();
    }
}