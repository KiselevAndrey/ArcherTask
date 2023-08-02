using UnityEngine;

namespace CodeBase.Character
{
    [System.Serializable]
    public class CharacterDamageable : IDamageable
    {
        [SerializeField, Range(1, 20)] private int _maxHealth;
        [SerializeField] private int _health;

        public void Init()
        {
            _health = _maxHealth;
        }

        public bool IsAlive => _health > 0;

        public int Health => _health;

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }
    }
}