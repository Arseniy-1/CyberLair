using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.CompositionRoot;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.ArenaSystem
{
    public class Wave
    {
        private readonly WaveConfig _config;
        private readonly EnemyFabric _fabric;

        private int _enemyCounter;

        public Wave(WaveConfig config, EnemyFabric fabric)
        {
            _config = config;
            _fabric = fabric;
        }
        
        public event Action<Wave> OnWaveFinished;

        public void Begin()
        {
            var enemies = new List<Enemy>();
            var random = new Random();
                
            foreach (KeyValuePair<Enemy, int> pair in _config.Enemies)
            {
                for (var i = 0; i < pair.Value; i++)
                {
                    enemies.Add(pair.Key);
                }
            }
            
            _enemyCounter = enemies.Count;
            enemies = enemies.OrderBy(x=> random.Next()).ToList();

            foreach (Enemy enemy in enemies.Select(en => _fabric.Create(en)))
            {
                enemy.OnDeath += HandleDeath;
            }
        }

        private void HandleDeath(Enemy enemy)
        {
            _enemyCounter--;
            enemy.OnDeath -= HandleDeath;
            
            if(_enemyCounter <= 0)
                OnWaveFinished?.Invoke(this);
        }
    }
}