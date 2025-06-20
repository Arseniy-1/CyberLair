using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UniRx;

namespace Project.Scripts.ArenaSystem
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private ExperienceParticle _experienceParticlePrefab;
        [SerializeField] private int _experienceAmount;

        [SerializeField] private HealthSpawner _healthSpawner;
        [SerializeField] private HealingHeart _heartPrefab;
        [SerializeField, Range(1, 100)] private int _heartSpawnChance;
        [SerializeField] private int _healAmount;
        
        [SerializeField] private List<WaveConfig> _wavesConfigs;
        
        [SerializeField] private Cage _cagePrefab;
        [SerializeField] private BossChest _bossChestPrefab;
        
        [SerializeField] private Effect _deathEffectPrefab;
        [SerializeField] private Effect _explosionEffectPrefab;
        
        private Cage _cageInstance;
        
        private EnemyDeathEffectsSpawner _enemyDeathEffectsSpawner;
        private ExplosionEffectsSpawner _explosionEffectsSpawner;
        
        private Queue<Wave> _waves;
        private Wave _currentWave;
        
        public event Action WavesDone;
        
        public IReadOnlyList<WaveConfig> WavesConfigs => _wavesConfigs;

        public void Initialize(Queue<Wave> waves, CancellationToken token)
        {
            _waves = waves;

            _enemyDeathEffectsSpawner = new EnemyDeathEffectsSpawner(_deathEffectPrefab, token);
            _explosionEffectsSpawner = new ExplosionEffectsSpawner(_explosionEffectPrefab, token); 
            
            _experienceSpawner.Initialize(_experienceAmount, _experienceParticlePrefab, token);
            _healthSpawner.Initialize(_heartPrefab, _heartSpawnChance, _healAmount, token);
        }

        public void Work()
        {
            StartNewWave();
        }

        public void OnDisable()
        {
            _currentWave?.Disable();
        }

        private void StartNewWave()
        {
            _currentWave?.Disable();
            
            if (_waves.Count == 0)
            {
                WavesDone?.Invoke();

                return;
            }

            _currentWave = _waves.Dequeue();

            _currentWave.OnWaveFinished += HandleWavesEnd;
            _currentWave.Begin();
        }

        private void HandleWavesEnd(Wave wave)
        {
            wave.OnWaveFinished -= HandleWavesEnd;

            StartNewWave();
        }
    }
}