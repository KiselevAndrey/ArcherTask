using System.Collections;
using UnityEngine;

namespace CodeBase.Weapon
{
    public abstract class Weapon : MonoBehaviour, IWeapon
    {
        [SerializeField, Min(0)] protected float Cooldown = 0.5f;

        private bool _isReloading = false;

        public bool CanAttack => _isReloading == false;

        public abstract bool AttackEnded { get; protected set; }

        public virtual void Attack(LayerMask damageLayer, int damage)
        {
            StartCoroutine(StartCooldown());
        }

        private IEnumerator StartCooldown()
        {
            _isReloading = true;
            yield return new WaitForSeconds(Cooldown);
            _isReloading = false;
        }
    }
}