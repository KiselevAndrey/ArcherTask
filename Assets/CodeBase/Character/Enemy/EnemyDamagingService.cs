using CodeBase.Character.Player;
using CodeBase.Infrastructure;
using System.Collections;
using UnityEngine;

namespace CodeBase.Character.Enemy
{
    [System.Serializable]
    public class EnemyDamagingService : CharacterDamagingService, IEnemyDamagingService
    {
        [SerializeField] private Vector2 _waitToNextAttackTime;
        [SerializeField] private LayerMask _attackLayerMask;

        private ICoroutineRunner _coroutineRunner;

        private bool _endPrepare = true;
        private bool _isBodyDamageCooldowning = false;

        public void Init(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _endPrepare = true;
            _isBodyDamageCooldowning = false;

            Init();
        }

        public bool CanAttack(Vector3 target)
        {
            TurnWeaponTo(target);

            Ray ray = new(WeaponPlace.position, WeaponPlace.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _attackLayerMask)){
                return hit.collider.GetComponent<IPlayer>() != null;
            }

            return false;
        }

        public override bool CanAttack()
        {
            return base.CanAttack() && _endPrepare;
        }

        public int BodyDamage()
        {
            if (_isBodyDamageCooldowning == false)
            {
                _coroutineRunner.StartCoroutine(BodyDamageCooldowning());
                return _damage;
            }

            return 0;
        }

        protected override void OnAttacked()
        {
            base.OnAttacked();

            _coroutineRunner.StartCoroutine(Preparing());
        }

        private IEnumerator Preparing()
        {
            _endPrepare = false;
            yield return new WaitForSeconds(Random.Range(_waitToNextAttackTime.x, _waitToNextAttackTime.y));
            _endPrepare = true;
        }

        private IEnumerator BodyDamageCooldowning()
        {
            _isBodyDamageCooldowning = true;
            yield return new WaitForSeconds(1);
            _isBodyDamageCooldowning = false;
        }
    }
}