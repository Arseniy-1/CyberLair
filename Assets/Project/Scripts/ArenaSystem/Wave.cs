using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Project.Scripts.Servises;
using Random = UnityEngine.Random;

namespace Project.Scripts.ArenaSystem
{
    public class Wave
    {
        private readonly WaveConfig _config;

        private readonly MainEnemySpawner _mainEnemySpawner;
        private readonly IReadOnlyList<Transform> _spawnPoints;

        private readonly List<ObjectWeightPair<Enemy>> _enemyWeights = new();
        private bool _isActive;
        private CancellationTokenSource _cancellationToken;

        public Wave(WaveConfig config, MainEnemySpawner mainEnemySpawner, List<Transform> spawnPoints)
        {
            _config = config;
            _mainEnemySpawner = mainEnemySpawner;
            _spawnPoints = spawnPoints;
            
            _enemyWeights.AddRange(_config.EnemyWeights);
        }
        
        public event Action<Wave> OnWaveFinished;
        public event Action<Enemy> EnemySpawned;

        public void Begin()
        {
            WaitingEnd();
            
            _mainEnemySpawner.ApplyModifier(_config.EnemyStatModifiers);
            
            _isActive = true;
            _cancellationToken = new CancellationTokenSource();

            SpawningEnemies(_enemyWeights);
        }

        private async UniTaskVoid SpawningEnemies(List<ObjectWeightPair<Enemy>> enemies)
        {
            enemies = enemies.OrderBy(x=> Random.value).ToList();

            var picker = new WeightedRandomPicker<Enemy>(enemies.Select(pair => pair.Prefab).ToList(),
                enemies.Select(pair => pair.Weight).ToList());
            
            while(_isActive)
            { 
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnDuration), cancellationToken: _cancellationToken.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                EnemyTypes preferredEnemy = picker.Pick().EnemyType;
                int enemyCount = Random.Range(_config.SpawnClusterSize.x, _config.SpawnClusterSize.y + 1);

                for (int i = 0; i < enemyCount; i++)
                {
                    Enemy enemy = _mainEnemySpawner.Spawn(preferredEnemy);
                    enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
                    enemy.ResetState();
                    EnemySpawned?.Invoke(enemy);
                }
            }
        } 

        private async UniTaskVoid WaitingEnd()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.WaveDuration));
            _cancellationToken.Cancel();
            OnWaveFinished?.Invoke(this);
        }
    }
}