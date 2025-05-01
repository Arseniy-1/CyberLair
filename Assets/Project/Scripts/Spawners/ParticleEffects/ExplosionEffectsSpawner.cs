using UniRx;
using UnityEngine;

public class ExplosionEffectsSpawner : Spawner<Effect>
{
    public ExplosionEffectsSpawner(Effect effect, CompositeDisposable compositeDisposable)
    {
        Prefab = effect;
        Pool = new ExplosionEffectsPool(Prefab, StartAmount);

        MessageBrokerHolder.Game.Receive<M_Exploded>().Subscribe((message) => SpawnEffect(message.Position))
            .AddTo(compositeDisposable);
    }

    private void SpawnEffect(Vector2 position)
    {
        var effect = Spawn();
        effect.transform.position = position;
    }
}