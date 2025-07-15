using System;
using System.Collections.Generic;
using System.Threading;
using Project.Scripts.Props;
using Project.Scripts.Spawners.Enemies;
using Project.Scripts.Spawners.Exp;
using Project.Scripts.Spawners.Health;
using Project.Scripts.Spawners.ParticleEffects;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private ExperienceParticle _experienceParticlePrefab;
        [SerializeField] private int _experienceAmount;

        [SerializeField] private HealthSpawner _healthSpawner;
        [SerializeField] private HealingHeart _heartPrefab;
        [SerializeField] [Range(1, 100)] private int _heartSpawnChance;
        [SerializeField] private int _healAmount;
        
        [SerializeField] private List<WaveConfig> _wavesConfigs;
        
        [SerializeField] private Cage _cagePrefab;
        [SerializeField] private BossChest _bossChestPrefab;
        
        [SerializeField] private Effect _deathEffectPrefab;
        [SerializeField] private Effect _explosionEffectPrefab;
        
        private Cage _cageInstance;
        
        private EnemyDeathEffectsSpawner _enemyDeathEffectsSpawner;
        private ExplosionEffectsSpawner _explosionEffectsSpawner;
        
        private WaveQueueFactory _waveQueueFactory;
        private Queue<Wave> _waves;
        private Wave _currentWave;
        
        public event Action WavesDone;

        private void OnDisable()
        {
            _currentWave?.Disable();
        }
        
        public void Initialize(MainEnemySpawner mainEnemySpawner, CancellationToken token)
        {
            _waveQueueFactory = new WaveQueueFactory();
            
            _waves = _waveQueueFactory.Create(_wavesConfigs, mainEnemySpawner);

            _enemyDeathEffectsSpawner = new EnemyDeathEffectsSpawner(_deathEffectPrefab, token);
            _explosionEffectsSpawner = new ExplosionEffectsSpawner(_explosionEffectPrefab, token); 
            
            _experienceSpawner.Initialize(_experienceAmount, _experienceParticlePrefab, token);
            _healthSpawner.Initialize(_heartPrefab, _heartSpawnChance, _healAmount, token);
        }

        public void Work()
        {
            StartNewWave();
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