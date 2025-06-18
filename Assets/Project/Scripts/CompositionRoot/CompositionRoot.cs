using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.SimpleLocalization.Scripts;
using Project.Scripts.ArenaSystem;
using Project.Scripts.MapGenerationSystem;
using Project.Scripts.Services;
using UnityEngine;
using YG;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Arena _arena;
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private BossHandler _bossHandler;
        [SerializeField] private Player _player;
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;
        [SerializeField] private EdgeSpawner _edgeSpawner;
        [SerializeField] private List<EnemyDespawner> _enemyDespawners;
        [SerializeField] private Level _level;
        [SerializeField] private Timer _timer;

        [SerializeField] private StatsBar _HealthBar;
        [SerializeField] private StatsText _HealthText;

        [SerializeField] private StatsBar _experienceBar;

        [SerializeField] private StatsBar _shieldBar;

        [SerializeField] private EndGameCanvas _endGameCanvas;
        [SerializeField] private Canvas _winGameCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TutorialWindow _tutorialView;

        private Coroutine _invulnerability;
        private CancellationTokenSource _cancellationToken;

        private void Awake()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            
            _edgeSpawner.SpawnOnEdges();
            _mapGenerator.Initialize();
            _bossHandler.Initialize(_player.transform, _cancellationToken.Token);

            _mainEnemySpawner.Initialize(_player, _edgeSpawner.EdgeObjects, _enemyDespawners);
            
            var waves = new Queue<Wave>(_arena.WavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner))
                .ToList());

            _arena.Initialize(waves, _cancellationToken.Token);
            _arena.Work();

            _level.Initialize(_player.ExperienceStorage);

            _shieldBar.Initialize(_player.PlayerStats.ShieldAmount);

            _HealthBar.Initialize(_player.PlayerStats.Health);
            _HealthText.Initialize(_player.PlayerStats.Health);

            _experienceBar.Initialize(_player.ExperienceStorage);

            LocalizationManager.Read();
            LocalizationManager.Language = YandexGame.lang;
        }

        private void OnEnable()
        {
            _player.OnDeath += OnPlayerDied;
            _arena.WavesDone += ShowWinScreen;
            YandexGame.RewardVideoEvent += OnRewarded;

            if (!YandexGame.savesData.isFirstSession) 
                return;
            
            _tutorialView.gameObject.SetActive(true);
            _tutorialView.OnFinished += OnTutorialFinished;
            PauseGame();

            YandexGame.savesData.isFirstSession = false;
            YandexGame.SaveProgress();
        }

        private void OnDisable()
        {
            _cancellationToken?.Cancel();
            
            _player.OnDeath -= OnPlayerDied;
            _arena.WavesDone -= ShowWinScreen;
            YandexGame.RewardVideoEvent -= OnRewarded;
        }

        private void OnTutorialFinished()
        {
            _tutorialView.gameObject.SetActive(false);
            _tutorialView.OnFinished -= OnTutorialFinished;
        }

        private void ShowWinScreen()
        {
            _gameCanvas.gameObject.SetActive(false);
            _winGameCanvas.gameObject.SetActive(true);

            PauseGame();

            if (_timer.CurrentSeconds <= YandexGame.savesData.bestTime)
                return;

            YandexGame.savesData.bestTime = _timer.CurrentSeconds;
            YandexGame.SaveProgress();
            YandexGame.NewLBScoreTimeConvert("Leaderboard", YandexGame.savesData.bestTime);
        }

        private void OnPlayerDied()
        {
            _gameCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
            _endGameCanvas.ShowStats(_timer.CurrentTime);
            
            PauseGame();
            
            if (_timer.CurrentSeconds <= YandexGame.savesData.bestTime)
                return;

            YandexGame.savesData.bestTime = _timer.CurrentSeconds;
            YandexGame.SaveProgress();
            YandexGame.NewLBScoreTimeConvert("Leaderboard", YandexGame.savesData.bestTime);
        }

        private void BringBackPlayer()
        {
            _player.PlayerStats.Health.Heal(_player.PlayerStats.Health.MaxHealth);

            if (_invulnerability != null)
                StopCoroutine(_invulnerability);

            _invulnerability = StartCoroutine(GivePlayerInvulnerability(_player));

            UnPauseGame();

            _gameCanvas.gameObject.SetActive(true);
            _endGameCanvas.gameObject.SetActive(false);
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            MessageBrokerHolder.Game.Publish(new M_GamePaused());
        }

        private void UnPauseGame()
        {
            Time.timeScale = 1;
            MessageBrokerHolder.Game.Publish(new M_GameUnpaused());
        }

        private IEnumerator GivePlayerInvulnerability(Player player)
        {
            float invulnerabilityTime = 2.5f;

            player.Collider2D.enabled = false;
            yield return new WaitForSeconds(invulnerabilityTime);
            player.Collider2D.enabled = true;
        }

        private void OnRewarded(int id)
        {
            if (id == (int)RewardedAdType.SecondChance)
            {
                BringBackPlayer();
            }
        }
    }
}