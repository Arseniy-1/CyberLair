using System;
using System.Collections.Generic;
using Project.Scripts.ArenaSystem;
using Project.Scripts.EnemySystem;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class HealthSpawner : Spawner<HealingHeart>
{
    private int _spawnChance;

    private List<Wave> _waves;
    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    private int _healAmount; 
    
        private void OnDisable()
    {
        foreach (var wave in _waves)
            wave.EnemySpawned -= OnEnemySpawned;

        foreach (var enemy in _spawnedEnemies)
            enemy.OnDestroyed -= OnEnemySpawned;
    }

    public void Initialize(List<Wave> waves, HealingHeart heartPrefab, int spawnChance, int healAmount)
    {
        Prefab = heartPrefab;
        Pool = new HealthPool(Prefab, StartAmount);

        _healAmount = healAmount;
        
        _waves = waves;
        _spawnChance = spawnChance;

        foreach (var wave in _waves)
            wave.EnemySpawned += OnEnemySpawned;
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
        {
            var particle = Spawn();
            particle.Initialize(_healAmount);
            particle.transform.position = enemy.transform.position;
        }
    }

    private bool CanSpawn()
    {
        return Random.Range(0, 100) < _spawnChance;
    }
}