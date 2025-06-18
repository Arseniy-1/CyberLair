using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.ArenaSystem;
using Project.Scripts.EnemySystem;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class HealthSpawner : Spawner<HealingHeart>
{
    private int _spawnChance;

    // private List<Wave> _waves;
    // private List<Enemy> _spawnedEnemies = new List<Enemy>();

    private int _healAmount; 
    
        private void OnDisable()
    {
        // foreach (var wave in _waves)
        //     wave.EnemySpawned -= OnEnemySpawned;
        //
        // foreach (var enemy in _spawnedEnemies)
        //     enemy.OnDestroyed -= OnEnemySpawned;
    }

    public void Initialize(List<Wave> waves, HealingHeart heartPrefab, int spawnChance, int healAmount, CancellationToken token)
    {
        Prefab = heartPrefab;
        Pool = new HealthPool(Prefab, StartAmount);

        _healAmount = healAmount;
        _spawnChance = spawnChance;
        
        // _waves = waves;

        // foreach (var wave in _waves)
        //     wave.EnemySpawned += OnEnemySpawned;
        
        MessageBrokerHolder.Enemy
            .Receive<M_EnemyDeath>()
            .Subscribe(message => OnEnemyDeath(message.Position))
            .AddTo(token);
    }

    // private void OnEnemySpawned(Enemy enemy)
    // {
    //     enemy.OnDestroyed += OnEnemyDeath;
    //     _spawnedEnemies.Add(enemy);
    // }

    private void OnEnemyDeath(Vector2 position)
    {
        // enemy.OnDestroyed -= OnEnemyDeath;
        // _spawnedEnemies.Remove(enemy);

        if (!CanSpawn())
            return;
        
        HealingHeart particle = Spawn();
        particle.Initialize(_healAmount);
        particle.transform.position = position;
    }

    private bool CanSpawn()
    {
        return Random.Range(0, 100) < _spawnChance;
    }
}