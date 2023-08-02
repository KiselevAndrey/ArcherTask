using UnityEngine;

namespace CodeBase.Character
{
    public abstract class CharacterMover
    {
        public Transform Transform { get; protected set; }

        public void LookAt(Vector3 targetPosition)
        {
            targetPosition.y = Transform.position.y;
            Transform.LookAt(targetPosition);
        }
    }
}