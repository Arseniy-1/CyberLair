using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private StatsText _experienceText;
        
        [SerializeField] private StatsBar _shieldBar;
        [SerializeField] private StatsText _shieldText;
        
        [SerializeField] private Canvas _endGameCanvas;
        [SerializeField] private Canvas _winGameCanvas;
        [SerializeField] private Canvas _gameCanvas;
        
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
            _shieldText.Initialize(_player.PlayerStats.ShieldAmount);
            
            _HealthBar.Initialize(_player.PlayerStats.Health);
            _HealthText.Initialize(_player.PlayerStats.Health);
            
            _experienceBar.Initialize(_player.ExperienceStorage);
            _experienceText.Initialize(_player.ExperienceStorage);
        }
        
        private void OnEnable()
        {
            _player.OnDeath += OnPlayerDied;
            _arena.WavesDone += ShowWinScreen;
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        private void OnDisable()
        {
            _player.OnDeath -= OnPlayerDied;
            _arena.WavesDone += ShowWinScreen;
            YandexGame.RewardVideoEvent -= OnRewarded;
        }

        private void ShowWinScreen()
        {
            _winGameCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        
        private void OnPlayerDied()
        {
            _gameCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void BringBackPlayer()
        {
            _player.PlayerStats.Health.Heal(_player.PlayerStats.Health.MaxHealth);
            StartCoroutine(GivePlayerInvulnerability(_player));
            
            Time.timeScale = 1;
            
            _gameCanvas.gameObject.SetActive(true);
            _endGameCanvas.gameObject.SetActive(false);
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