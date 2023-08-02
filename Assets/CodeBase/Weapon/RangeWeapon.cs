using UnityEngine;
using static Lean.Pool.LeanPool;

namespace CodeBase.Weapon
{
    public class RangeWeapon : Weapon
    {
        [SerializeField] private Projectile _projectile;
        [SerializeField] private Transform _projectileSpawnPoint;

        public override bool AttackEnded { get => true; protected set => throw new System.NotImplementedException(); }

        public override void Attack(LayerMask damageLayer, int damage)
        {
            var projectile = Spawn(_projectile, _projectileSpawnPoint);
            projectile.transform.parent = null;
            projectile.Init(OnProjectileHitEnded, damageLayer, damage);

            base.Attack(damageLayer, damage);
        }

        private void OnProjectileHitEnded(Projectile projectile)
        {
            Despawn(projectile.gameObject);
        }
    }
}