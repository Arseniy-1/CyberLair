using System;
using System.Collections.Generic;
using Project.Scripts.ArenaSystem;
using Project.Scripts.EnemySystem;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ExperienceSpawner : Spawner<ExperienceParticle>
{
    private List<Wave> _waves;
    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    [SerializeField] private int _experienceAmount;

    private void OnDisable()
    {
        foreach (var wave in _waves)
            wave.EnemySpawned -= OnEnemySpawned;

        foreach (var enemy in _spawnedEnemies)
            enemy.OnDestroyed -= OnEnemySpawned;
    }

    public void Initialize(List<Wave> waves, int experienceAmount, ExperienceParticle prefab)
    {
        Prefab = prefab;
        _experienceAmount = experienceAmount;
        
        Pool = new ExperiencePaticlePool(Prefab, StartAmount);
        
        _waves = waves;

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
            particle.Initialize(_experienceAmount);
            particle.transform.position = enemy.transform.position;
        }
    }

    private bool CanSpawn()
    {
        return true;
    }
}