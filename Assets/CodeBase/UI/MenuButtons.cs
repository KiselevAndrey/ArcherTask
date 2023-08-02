using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [Serializable]
    public class MenuButtons
    {
        [SerializeField] private Button _start;
        [SerializeField] private Button _home;

        private Action _startGameClick;
        private Action _toMenuClick;

        public void Init(Action startGameClick, Action toMenuClick)
        {
            _startGameClick = startGameClick;
            _toMenuClick = toMenuClick;
        }

        public void Subscribe()
        {
            _start.onClick.AddListener(OnStartButtonClick);
            _home.onClick.AddListener(OnHomeButtonClick);
        }

        public void Unsubscribe()
        {
            _start.onClick.RemoveListener(OnStartButtonClick);
            _home.onClick.RemoveListener(OnHomeButtonClick);
        }

        private void OnStartButtonClick() =>
            _startGameClick?.Invoke();

        private void OnHomeButtonClick() =>
            _toMenuClick?.Invoke();
    }
}