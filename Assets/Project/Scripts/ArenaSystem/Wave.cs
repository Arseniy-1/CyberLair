using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Project.Scripts.EnemySystem;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Project.Scripts.Servises;
using Sirenix.Utilities;
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
            
            _cancellationToken?.Cancel();
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

            WaitingEnd(_cancellationToken.Token).Forget();
            
            SpawningEnemies(_enemyWeights, _cancellationToken.Token).Forget();
        }

        public void Disable()
        {
            _cancellationToken?.Cancel();
        }

        private async UniTaskVoid SpawningEnemies(List<ObjectWeightPair<Enemy>> enemies, CancellationToken token)
        {
            var picker = new WeightedRandomPicker<Enemy>(enemies.Select(pair => pair.Prefab).ToList(),
                enemies.Select(pair => pair.Weight).ToList());
            
            while(token.IsCancellationRequested == false)
            { 
                await UniTask.Delay(TimeSpan.FromSeconds(_config.SpawnDuration), cancellationToken: token);

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

        private async UniTaskVoid WaitingEnd(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.WaveDuration), cancellationToken: token);
            
            OnWaveFinished?.Invoke(this);
        }

        private void HandleBossDeath(Enemy enemy)
        {
            enemy.OnDestroyed -= HandleBossDeath;
            
            MessageBrokerHolder.Enemy
                .Publish(new M_BossDeath(_bossInstance));
        }
    }
}