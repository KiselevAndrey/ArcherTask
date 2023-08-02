using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.World
{
    public class Map : MonoBehaviour
    {
        [SerializeField] private Transform _leftDownPoint;
        [SerializeField] private Transform _rightUpPoint;

        public Vector3 GetRandomPoint() =>
            RandomPosition.InCube(_leftDownPoint.position, _rightUpPoint.position);
    }
}