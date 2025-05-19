using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Scripts.EnemySystem;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Project.Scripts.Servises;
using Sirenix.Utilities;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace Project.Scripts.ArenaSystem
{
    public class Wave
    {
        private readonly WaveConfig _config;

        private readonly MainEnemySpawner _mainEnemySpawner;

        private readonly List<ObjectWeightPair<Enemy>> _enemyWeights = new();
        private CancellationTokenSource _cancellationToken;

        private Enemy _bossInstance;

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
            if(_config.EnemyStatModifiers.Value > 0)
                _mainEnemySpawner.ApplyModifier(_config.EnemyStatModifiers);
            
            _cancellationToken = new CancellationTokenSource();

            if (_config.Boss != false)
            {
                _bossInstance = _mainEnemySpawner.Spawn(_config.Boss.EnemyType);
                
                _bossInstance.OnDestroyed += HandleBossDeath;
                MessageBrokerHolder.Enemy.Publish(new M_BossSpawned(_bossInstance));
            }

            if (_enemyWeights.IsNullOrEmpty())
            {
                OnWaveFinished?.Invoke(this);
                
                return;
            }

            WaitingEnd();
            
            SpawningEnemies(_enemyWeights);
        }

        private async UniTaskVoid SpawningEnemies(List<ObjectWeightPair<Enemy>> enemies)
        {
            var picker = new WeightedRandomPicker<Enemy>(enemies.Select(pair => pair.Prefab).ToList(),
                enemies.Select(pair => pair.Weight).ToList());
            
            while(_cancellationToken.Token.IsCancellationRequested == false)
            { 
                await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnDuration), cancellationToken: _cancellationToken.Token);

                EnemyTypes preferredEnemy = picker.Pick().EnemyType;
                int enemyCount = Random.Range(_config.SpawnClusterSize.x, _config.SpawnClusterSize.y + 1);

                for (int i = 0; i < enemyCount; i++)
                {
                    Enemy enemy = _mainEnemySpawner.Spawn(preferredEnemy);
                    
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

        private void HandleBossDeath(Enemy enemy)
        {
            enemy.OnDestroyed -= HandleBossDeath;
            
            MessageBrokerHolder.Enemy.Publish(new M_BossDeath(_bossInstance));
        }
    }
}