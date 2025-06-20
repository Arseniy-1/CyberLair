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

    private int _healAmount; 

    public void Initialize(HealingHeart heartPrefab, int spawnChance, int healAmount, CancellationToken token)
    {
        Prefab = heartPrefab;
        Pool = new HealthPool(Prefab, StartAmount);

        _healAmount = healAmount;
        _spawnChance = spawnChance;
        
        MessageBrokerHolder.Enemy
            .Receive<M_EnemyDeath>()
            .Subscribe(message => OnEnemyDeath(message.Position))
            .AddTo(token);
    }

    private void OnEnemyDeath(Vector2 position)
    {
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