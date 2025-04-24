using System;
using UniRx;
using UnityEngine;

[Serializable]
public class EffectsSpawner : Spawner<Effect>
{
    private readonly CompositeDisposable _disposable;
    
    public EffectsSpawner(Effect effect, CompositeDisposable compositeDisposable)
    {
        Prefab = effect;
        Pool = new EffectsPool(Prefab, StartAmount);
        
        _disposable = new CompositeDisposable();
        MessageBrokerHolder.Enemy.Receive<M_EnemyDeath>().Subscribe((message) => HandleEnemyDeath(message.Position))
            .AddTo(_disposable);
    }

    private void HandleEnemyDeath(Vector2 position)
    {
        var effect = Spawn();
        effect.transform.position = position;
    }
}