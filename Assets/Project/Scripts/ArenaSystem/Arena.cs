using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.ArenaSystem
{
    public class Arena : MonoBehaviour
    {
        [SerializeField] private ExperienceSpawner _experienceSpawner;
        [SerializeField] private ExperienceParticle _experienceParticlePrefab;
        [SerializeField, Range(1, 10)] private float _experienceMultiplier;

        [SerializeField] private HealthSpawner _healthSpawner;
        [SerializeField] private HealingHeart _heartPrefab;
        [SerializeField, Range(1, 100)] private int _heartSpawnChance;
        
        [SerializeField] private List<WaveConfig> _wavesConfigs;

        private Queue<Wave> _waves;

        public event Action WavesDone;
        
        public IReadOnlyList<WaveConfig> WavesConfigs => _wavesConfigs;

        public void Initialize(Queue<Wave> waves)
        {
            _waves = waves;
            
            _experienceSpawner.Initialize(waves.ToList(), _experienceMultiplier, _experienceParticlePrefab);
            _healthSpawner.Initialize(waves.ToList(), _heartPrefab, _heartSpawnChance);
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