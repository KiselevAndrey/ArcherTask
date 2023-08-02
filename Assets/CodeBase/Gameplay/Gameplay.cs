using CodeBase.Character.Player;
using CodeBase.Options;
using CodeBase.UI;
using CodeBase.World;
using UnityEngine;

namespace CodeBase.Gameplay
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour _player;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private Menu _menu;
        [SerializeField] private Gate _gate;
        [SerializeField] private GameplayOptionsSO _optionsSO;

        private int _coins = 0;

        public void PrepareGame()
        {
            Lean.Pool.LeanPool.DespawnAll();

            _player.PrepareGame();
            _enemySpawner.PrepareGame();
            _gate.PrepareGame();

            EnableGame(true);
        }

        public void StartGame()
        {
            Time.timeScale = 1;
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        private void Start()
        {
            EndGame();

            _optionsSO.EnemyCount = 1;
        }

        private void OnEnable()
        {
            _player.OnFail += OnPlayerFail;
            _player.OnHealthChanged += OnPlayerHealthChanged;
            _enemySpawner.OnFail += OnEnemyFail;
            _enemySpawner.OnEnemyDie += OnEnemyDie;
            _gate.OnFail += OnGateFail;
        }

        private void OnDisable()
        {
            _player.OnFail -= OnPlayerFail;
            _player.OnHealthChanged -= OnPlayerHealthChanged;
            _enemySpawner.OnFail -= OnEnemyFail;
            _enemySpawner.OnEnemyDie -= OnEnemyDie;
            _gate.OnFail -= OnGateFail;
        }

        private void OnPlayerFail()
        {
            _optionsSO.EnemyCount = (int)(_optionsSO.EnemyCount * 0.5f);
            EndGame();
        }

        private void OnPlayerHealthChanged(int value) =>
            _menu.UpdateHealth(value);

        private void OnEnemyFail() =>
            _gate.Open();

        private void OnGateFail()
        {
            _optionsSO.EnemyCount *= 2;
            EndGame();
        }

        private void OnEnemyDie() =>
            _menu.UpdateCoinCount(++_coins);

        private void EndGame()
        {
            Time.timeScale = 0;
            _menu.EndGame();
            EnableGame(false);
        }

        private void EnableGame(bool enable)
        {
            _player.gameObject.SetActive(enable);
            _enemySpawner.gameObject.SetActive(enable);
        }
    }
}