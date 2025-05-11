using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        
        [SerializeField] private StatsBar _HealthBar;
        [SerializeField] private StatsText _HealthText;
        
        [SerializeField] private StatsBar _experienceBar;
        
        [SerializeField] private StatsBar _shieldBar;
        
        [SerializeField] private Canvas _endGameCanvas;
        [SerializeField] private Canvas _winGameCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TutorialWindow _tutorialView;
        
        private void Awake()
        {
            _edgeSpawner.SpawnOnEdges();
            _mapGenerator.Initialize();
            _bossHandler.Initialize(_player.transform);
            
            _mainEnemySpawner.Initialize(_player, _edgeSpawner.EdgeObjects.ToList(), _enemyDespawners);
            var waves = new Queue<Wave>(_arena.WavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner)).ToList());

            _arena.Initialize(waves);
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

            if (true)
            {
                _tutorialView.gameObject.SetActive(true);
                _tutorialView.OnFinished += OnTutorialFinished;
                PauseGame();

                YandexGame.savesData.isFirstSession = false;
                YandexGame.SaveProgress();
            }
        }

        private void OnDisable()
        {
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
            _winGameCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
            PauseGame();
        }
        
        private void OnPlayerDied()
        {
            _gameCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
            PauseGame();
        }

        private void BringBackPlayer()
        {
            _player.PlayerStats.Health.Heal(_player.PlayerStats.Health.MaxHealth);
            StartCoroutine(GivePlayerInvulnerability(_player));

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
            float invulnerabilityTime = 5f;
            
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