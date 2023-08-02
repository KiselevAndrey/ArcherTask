using CodeBase.Weapon;
using UnityEngine;

namespace CodeBase.Character
{
    public abstract class CharacterDamagingService : IDamagingService
    {
        [field: SerializeField, Range(1, 5)] protected int _damage = 1;
        [SerializeField] protected Transform WeaponPlace;
        [SerializeField] private LayerMask _damageLayer;

        private IWeapon _weapon;

        public bool AttackEnded => _weapon.AttackEnded;

        public void Init()
        {
            _weapon = WeaponPlace.GetComponentInChildren<IWeapon>();
            if (_weapon == null)
                Debug.LogError($"{WeaponPlace.parent} doesn't have a weapon");
        }

        public void TryAttack(Vector3 target)
        {
            if (CanAttack())
            {
                TurnWeaponTo(target);
                _weapon.Attack(_damageLayer, _damage);
                OnAttacked();
            }
        }

        public virtual bool CanAttack()
        {
            return _weapon.CanAttack;
        }

        protected virtual void OnAttacked() { }

        protected void TurnWeaponTo(Vector3 target)
        {
            target.y = WeaponPlace.position.y;
            WeaponPlace.LookAt(target);
        }
    }
}