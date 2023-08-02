using UnityEngine;

namespace CodeBase.Character.Player
{
    [System.Serializable]
    public class PlayerMover : CharacterMover
    {
        [SerializeField] private float _speed;

        private Rigidbody _rigidbody;

        public void Init(Rigidbody rigidbody, Transform transform)
        {
            _rigidbody = rigidbody;
            Transform = transform;
        }

        public void Move(Vector2 inputVector)
        {
            _rigidbody.velocity = new( inputVector.x * _speed, _rigidbody.velocity.y, inputVector.y * _speed);
             Transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }
}