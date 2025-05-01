using System;
using UniRx;
using UnityEngine;

[Serializable]
public class EnemyDeathEffectsSpawner : Spawner<Effect>
{
    public EnemyDeathEffectsSpawner(Effect effect, CompositeDisposable compositeDisposable)
    {
        Prefab = effect;
        Pool = new DeathEffectsPool(Prefab, StartAmount);
        
        MessageBrokerHolder.Enemy.Receive<M_EnemyDeath>().Subscribe((message) => HandleEnemyDeath(message.Position))
            .AddTo(compositeDisposable);
    }

    private void HandleEnemyDeath(Vector2 position)
    {
        var effect = Spawn();
        effect.transform.position = position;
    }
}