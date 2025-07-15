using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.EnemySystem;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using Project.Scripts.Services;
using Project.Scripts.Services.Enum;
using Project.Scripts.Spawners.Enemies;
using Sirenix.Utilities;
using Random = UnityEngine.Random;

namespace Project.Scripts.ArenaSystem
{
    public class Wave
    {
        private readonly WaveConfig _config;
        private readonly MainEnemySpawner _mainEnemySpawner;
        private readonly List<ObjectWeightPair<Enemy>> _enemyWeights;
        
        private CancellationTokenSource _cancellationToken;

        public Wave(WaveConfig config, MainEnemySpawner mainEnemySpawner)
        {
            _config = config;
            _mainEnemySpawner = mainEnemySpawner;
            _enemyWeights = new List<ObjectWeightPair<Enemy>>();
            
            _enemyWeights.AddRange(_config.EnemyWeights);
        }
        
        public event Action<Wave> OnWaveFinished;

        public void Begin()
        {
            if(_config.EnemyStatModifiers.Value > 0)
                _mainEnemySpawner.ApplyModifier(_config.EnemyStatModifiers);
            
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();

            if (_config.Boss != false)
            {
                MessageBrokerHolder.Enemy
                    .Publish(new M_BossSpawned(_mainEnemySpawner.Spawn(_config.Boss.EnemyType)));
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
                }
            }
        } 

        private async UniTaskVoid WaitingEnd(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_config.WaveDuration), cancellationToken: token);
            
            OnWaveFinished?.Invoke(this);
        }
    }
}