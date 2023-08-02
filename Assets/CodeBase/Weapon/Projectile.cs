using CodeBase.Character;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.Weapon
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class Projectile : MonoBehaviour, IProjectile
    {
        [SerializeField, Min(0)] private float _speed;
        [SerializeField, Range(0, 3)] private float _timeFromHitToDespawn;
        [SerializeField, Range(1, 5)] private float _lifeTime;

        private Rigidbody _rigidbody;
        private Action<Projectile> _onHitEnded;
        private Coroutine _lifeTimeCoroutine;

        private bool _isEnable;
        private bool _isDamaging;
        private int _damage;

        public void Init(Action<Projectile> onHitEnded, LayerMask damageLayer, int damage)
        {
            _onHitEnded = onHitEnded;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = transform.forward * _speed;
            _lifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine(_lifeTime));
            gameObject.layer = (int)Mathf.Log(damageLayer.value, 2);
            _damage = damage;
        }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _isEnable = true;
            _isDamaging = true;
        }

        private void OnDisable()
        {
            _rigidbody.velocity = Vector3.zero;

            if (_lifeTimeCoroutine != null)
                StopCoroutine(_lifeTimeCoroutine);
        }

        private void OnTriggerEnter(Collider other)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.isKinematic = true;
            transform.parent = other.transform;

            StopCoroutine(_lifeTimeCoroutine);
            _lifeTimeCoroutine = StartCoroutine(LifeTimeCoroutine(_timeFromHitToDespawn));

            if (other.TryGetComponent(out IDamageable damageable))
            {
                if(_isDamaging)
                {
                    damageable.TakeDamage(_damage);
                    _isDamaging = false;
                }
            }
        }

        private void EndHit()
        {
            if(_isEnable)
            {
                _isEnable = false;
                _onHitEnded(this);
            }
        }

        private IEnumerator LifeTimeCoroutine(float lifeTime)
        {
            yield return new WaitForSeconds(lifeTime);
            EndHit();
        }
    }
}