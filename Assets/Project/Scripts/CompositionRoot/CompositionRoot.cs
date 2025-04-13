using System.Collections.Generic;
using System.Linq;
using Project.Scripts.ArenaSystem;
using UnityEngine;
using YG;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Arena _arena;
        [SerializeField] private Player _player;
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;
        [SerializeField] private EdgeSpawner _edgeSpawner;
        [SerializeField] private Level _level;
        
        [SerializeField] private StatsBar _HealthBar;
        [SerializeField] private StatsText _HealthText;
        
        [SerializeField] private StatsBar _experienceBar;
        [SerializeField] private StatsText _experienceText;
        
        [SerializeField] private StatsBar _shieldBar;
        [SerializeField] private StatsText _shieldText;
        
        [SerializeField] private Canvas _endGameCanvas;
        [SerializeField] private Canvas _gameCanvas;

        private bool _secondChanceTaken = false;
        
        private void OnEnable()
        {
            _player.OnDeath += OnPlayerDied;
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        private void OnDisable()
        {
            _player.OnDeath -= OnPlayerDied;
            YandexGame.RewardVideoEvent -= OnRewarded;
        }
        
        private void Awake()
        {
            _edgeSpawner.SpawnOnEdges();
            
            _mainEnemySpawner.Initialize(_player);
            var waves = new Queue<Wave>(_arena.WavesConfigs
                .Select(config => new Wave(config, _mainEnemySpawner, _edgeSpawner.EdgeObjects.ToList())).ToList());

            _arena.Initialize(waves, _player.transform);
            _arena.Work();
            
            _level.Initialize(_player.ExperienceStorage);
            
            _shieldBar.Initialize(_player.PlayerStats.ShieldAmount);
            _shieldText.Initialize(_player.PlayerStats.ShieldAmount);
            
            _HealthBar.Initialize(_player.PlayerStats.Health);
            _HealthText.Initialize(_player.PlayerStats.Health);
            
            _experienceBar.Initialize(_player.ExperienceStorage);
            _experienceText.Initialize(_player.ExperienceStorage);
        }
        
        private void OnPlayerDied()
        {
            _gameCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
            
            YandexGame.CloseVideoEvent += BringBackPlayer;
        }

        private void BringBackPlayer()
        {
            YandexGame.CloseVideoEvent += BringBackPlayer;
            
            if (_secondChanceTaken)
                return;
            
            _player.PlayerStats.Health.Heal(_player.PlayerStats.Health.MaxHealth);
            
            Time.timeScale = 1;
            
            _gameCanvas.gameObject.SetActive(true);
            _endGameCanvas.gameObject.SetActive(false);
        }
        
        private void OnRewarded(int id)
        {
            if (id == (int)RewardedAdType.SecondChance)
            {
                _secondChanceTaken = true;
            }
        }
    }
}