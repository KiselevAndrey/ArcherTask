using CodeBase.Infrastructure;
using CodeBase.Options;
using CodeBase.UI.Visibility;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class Menu : MonoBehaviour, ICoroutineRunner
    {
        [SerializeField] private Gameplay.Gameplay _gameplay;
        [SerializeField] private GameplayOptionsSO _optionsSO;

        [Space]
        [SerializeField] private MenuButtons _buttons;
        [SerializeField] private CountdownBehaviour _countdown;

        [Header("Texts")]
        [SerializeField] private TMP_Text _coinText;
        [SerializeField] private TMP_Text _enemyCountText;
        [SerializeField] private TMP_Text _healthText;

        [Header("Canvas Group Controllers")]
        [SerializeField] private CanvasGroupController _menuCanvas;
        [SerializeField] private CanvasGroupController _homeButtonCanvas;
        [SerializeField] private CanvasGroupController _joystickCanvas;

        public void EndGame()
        {
            _joystickCanvas.Hide();
            ShowMenu();
        }

        public void UpdateCoinCount(int value) =>
            _coinText.text = value.ToString();
        public void UpdateHealth(int value) =>
            _healthText.text = value.ToString();

        private void Awake()
        {
            _buttons.Init(StartGame, Pause);
        }

        private void Start()
        {
            EndGame();
        }

        private void OnEnable()
        {
            _buttons.Subscribe();
        }

        private void OnDisable()
        {
            _buttons.Unsubscribe();
        }

        private void StartGame()
        {
            _joystickCanvas.Show();
            HideMenu();
        }

        private void Pause()
        {
            _gameplay.Pause();
            ShowMenu();
        }

        private void ShowMenu()
        {
            _menuCanvas.Show();
            _homeButtonCanvas.Hide();
            _enemyCountText.text = _optionsSO.EnemyCount.ToString();
        }

        private void HideMenu()
        {
            _gameplay.PrepareGame();
            _menuCanvas.Hide();
            _countdown.Start(this, OnCountdownEnded);
        }

        private void OnCountdownEnded()
        {
            _gameplay.StartGame();
            _homeButtonCanvas.Show();
        }
    }
}