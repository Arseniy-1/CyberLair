using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class ExplosionEffectsSpawner : Spawner<Effect>
{
    public ExplosionEffectsSpawner(Effect effect, CancellationToken token)
    {
        Prefab = effect;
        Pool = new ExplosionEffectsPool(Prefab, StartAmount);

        MessageBrokerHolder.Game
            .Receive<M_Exploded>()
            .Subscribe(message => SpawnEffect(message.Position))
            .AddTo(token);
    }

    private void SpawnEffect(Vector2 position)
    {
        Effect effect = Spawn();
        effect.transform.position = position;
    }
}