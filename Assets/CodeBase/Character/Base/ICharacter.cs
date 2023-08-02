using UnityEngine;

namespace CodeBase.Character
{
    public interface ICharacter : IDamageable
    {
        public Transform Transform { get; }
    }
}