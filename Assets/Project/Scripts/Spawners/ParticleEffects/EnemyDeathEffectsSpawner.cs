using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Spawners.ParticleEffects
{
    [Serializable]
    public class EnemyDeathEffectsSpawner : Spawner<Effect>
    {
        public EnemyDeathEffectsSpawner(Effect effect, CancellationToken token)
        {
            Prefab = effect;
            Pool = new DeathEffectsPool(Prefab, StartAmount);
        
            MessageBrokerHolder.Enemy
                .Receive<M_EnemyDeath>()
                .Subscribe(message => HandleEnemyDeath(message.Position))
                .AddTo(token);
        }

        private void HandleEnemyDeath(Vector2 position)
        {
            Effect effect = Spawn();
            effect.transform.position = position;
        }
    }
}