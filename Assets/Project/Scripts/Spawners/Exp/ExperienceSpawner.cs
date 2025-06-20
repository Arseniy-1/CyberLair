using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.ArenaSystem;
using Project.Scripts.EnemySystem;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

[Serializable]
public class ExperienceSpawner : Spawner<ExperienceParticle>
{
    [SerializeField] private int _experienceAmount;

    [SerializeField] private float _minPushForce = 0.0005f;
    [SerializeField] private float _maxPushForce = 0.001f;

    public void Initialize(int experienceAmount, ExperienceParticle prefab, CancellationToken token)
    {
        Prefab = prefab;
        _experienceAmount = experienceAmount;

        Pool = new ExperiencePaticlePool(Prefab, StartAmount);
        
        MessageBrokerHolder.Enemy
            .Receive<M_EnemyDeath>()
            .Subscribe(message => OnEnemyDeath(message.Position))
            .AddTo(token);
    }

    private void OnEnemyDeath(Vector2 position)
    {
        var particle = Spawn();
        particle.Initialize(_experienceAmount);

        particle.transform.position = position;

        IMoveable interactable = particle;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float forceMagnitude = Random.Range(_minPushForce, _maxPushForce);
        interactable.Rigidbody2D.AddForce(randomDirection * forceMagnitude, ForceMode2D.Impulse);
    }

    private Vector2 GetRandomPosition(Transform targetTransform, float radius = 3f)
    {
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        return (Vector2)targetTransform.position + randomPoint;
    }
}