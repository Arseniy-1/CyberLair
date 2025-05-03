using System;
using System.Collections.Generic;
using System.Linq;
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
        
        private readonly CompositeDisposable _disposable = new();
        
        private Queue<Wave> _waves;
        
        public event Action WavesDone;
        
        public IReadOnlyList<WaveConfig> WavesConfigs => _wavesConfigs;

        public void Initialize(Queue<Wave> waves)
        {
            _waves = waves;

            _enemyDeathEffectsSpawner = new EnemyDeathEffectsSpawner(_deathEffectPrefab, _disposable);
            _explosionEffectsSpawner = new ExplosionEffectsSpawner(_explosionEffectPrefab, _disposable); 
            
            _experienceSpawner.Initialize(waves.ToList(), _experienceAmount, _experienceParticlePrefab);
            _healthSpawner.Initialize(waves.ToList(), _heartPrefab, _heartSpawnChance, _healAmount);
        }

        public void Work()
        {
            StartNewWave();
        }

        private void StartNewWave()
        {
            if (_waves.Count == 0)
            {
                WavesDone?.Invoke();

                return;
            }

            Wave wave = _waves.Dequeue();

            wave.OnWaveFinished += HandleWavesEnd;
            wave.Begin();
        }

        private void HandleWavesEnd(Wave wave)
        {
            wave.OnWaveFinished -= HandleWavesEnd;

            StartNewWave();
        }
    }
}