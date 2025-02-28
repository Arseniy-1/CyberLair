using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Scripts.EnemySystem;
using Cysharp.Threading.Tasks;
using Project.Scripts.Servises;
using Random = UnityEngine.Random;

namespace Project.Scripts.ArenaSystem
{
    public class Wave
    {
        private readonly WaveConfig _config;

        private readonly MainEnemySpawner _mainEnemySpawner;

        private readonly List<ObjectWeightPair<Enemy>> _enemyWeights = new();
        private bool _isActive;
        private CancellationTokenSource _cancellationToken;

        public Wave(WaveConfig config, MainEnemySpawner mainEnemySpawner)
        {
            _config = config;
            _mainEnemySpawner = mainEnemySpawner;
            
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
                EnemyTypes preferredEnemy = picker.Pick().EnemyType;
                int enemyCount = Random.Range(_config.SpawnClusterSize.x, _config.SpawnClusterSize.y + 1);

                for (int i = 0; i < enemyCount; i++)
                {
                    Enemy enemy = _mainEnemySpawner.Spawn(preferredEnemy);
                    
                    enemy.ResetState();
                    EnemySpawned?.Invoke(enemy);
                }
                
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnDuration), cancellationToken: _cancellationToken.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
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