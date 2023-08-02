using CodeBase.Character.Player;
using CodeBase.Gameplay;
using System;
using UnityEngine;

namespace CodeBase.World
{
    public class Gate : MonoBehaviour, IGameplayObserver
    {
        public event Action OnFail;

        public void PrepareGame() =>
            gameObject.SetActive(false);

        public void Open() =>
            gameObject.SetActive(true);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPlayer player))
                OnFail?.Invoke();
        }
    }
}