using System;
using System.Collections.Generic;
using Project.Scripts.ArenaSystem;
using Project.Scripts.EnemySystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class ExperienceSpawner : Spawner<ExperienceParticle>
{
    private List<Wave> _waves;
    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    [SerializeField] private int _experienceAmount;
    [SerializeField] private float _spawnRadius = 1;

    [SerializeField] private float _minPushForce = 0.0005f;
    [SerializeField] private float _maxPushForce = 0.001f;

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

        for (int i = 0; i < enemy.EnemyStats.Experience; i++)
        {
            var particle = Spawn();
            particle.Initialize(_experienceAmount);

            particle.transform.position = enemy.transform.position;

            IMoveable interactable = particle as IMoveable;

            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            float forceMagnitude = Random.Range(_minPushForce, _maxPushForce);
            interactable.Rigidbody2D.AddForce(randomDirection * forceMagnitude, ForceMode2D.Impulse);
        }
    }

    private Vector2 GetRandomPosition(Transform targetTransform, float radius = 3f)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        return (Vector2)targetTransform.position + randomPoint;
    }
}