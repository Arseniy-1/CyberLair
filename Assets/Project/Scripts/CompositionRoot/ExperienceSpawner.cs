using System.Collections.Generic;
using Project.Scripts.ArenaSystem;
using Project.Scripts.EnemySystem;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class ExperienceSpawner : Spawner<ExperienceParticle>
    {
        [SerializeField, Range(1, 100)] private int _chanceToSpawn;

        private List<Wave> _waves;
        private List<Enemy> _spawnedEnemies;
        
        private void OnDisable()
        {
            foreach (var wave in _waves)
                wave.EnemySpawned -= OnEnemySpawned;

            foreach (var enemy in _spawnedEnemies)
                enemy.OnDestroyed -= OnEnemySpawned;
        }

        public void Initialize(List<Wave> waves)
        {
            _waves = waves;
            
            foreach (var wave in _waves)
                wave.EnemySpawned += OnEnemySpawned;
            
            // Pool = 
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            enemy.OnDestroyed += OnEnemyDestroyed;
            _spawnedEnemies.Add(enemy);
        }

        private void OnEnemyDestroyed(Enemy enemy)
        {
            enemy.OnDestroyed -= OnEnemyDestroyed;
            _spawnedEnemies.Remove(enemy);
            
            if (CanSpawn())
                Spawn();
        }

        private bool CanSpawn()
        {
            return true;
        }
    }
}