using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Project.Scripts.Servises;
using Random = UnityEngine.Random;

namespace Project.Scripts.ArenaSystem
{
    public class Wave
    {
        private const int SecondMultiplier = 1000;
        
        private readonly WaveConfig _config;

        private readonly MainEnemySpawner _mainEnemySpawner;
        private readonly IReadOnlyList<Transform> _spawnPoints;

        private readonly List<ObjectWeightPair<Enemy>> _enemyWeights = new();
        // private int _enemyCounter;

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
            
            // var enemyPrefabs = new List<Enemy>();
            //     
            // foreach (KeyValuePair<Enemy, int> pair in _config.Enemies)
            // {
            //     for (var i = 0; i < pair.Value; i++)
            //     {
            //         enemyPrefabs.Add(pair.Key);
            //     }
            // }

            SpawningEnemies(_enemyWeights);
        }

        private async UniTaskVoid SpawningEnemies(List<ObjectWeightPair<Enemy>> enemies)
        {
            // _enemyCounter = enemies.Count;
            enemies = enemies.OrderBy(x=> Random.value).ToList();

            var picker = new WeightedRandomPicker<Enemy>(enemies.Select(pair => pair.Prefab).ToList(),
                enemies.Select(pair => pair.Weight).ToList());

            foreach (var enemyPrefab in enemies.Select(pair => pair.Prefab))
            {
                int delay = Convert.ToInt32(_config.SpawnDuration * SecondMultiplier);
                await UniTask.Delay(delay);
                
                Enemy enemy = _mainEnemySpawner.Spawn(picker.Pick().EnemyType);
                enemy.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;
                enemy.ResetState();
                // enemy.OnDestroyed += HandleDeath;
                EnemySpawned?.Invoke(enemy);
            }
        } 

        private async UniTaskVoid WaitingEnd()
        {
            await UniTask.Delay(_config.WaveDuration * SecondMultiplier);
            OnWaveFinished?.Invoke(this);
        }

        // private void HandleDeath(Enemy enemy)
        // {
        //     _enemyCounter--;
        //     enemy.OnDestroyed -= HandleDeath;
        //     
        //     if(_enemyCounter <= 0)
        //         OnWaveFinished?.Invoke(this);
        // }
    }
}