using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Prefabs.Configs.Skills.StreamingEnergy;
using Project.Scripts.Interfaces;
using Project.Scripts.MessageBroker;
using Project.Scripts.MessageBroker.EnemyMessageBrokers;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Spawners.StreamingEnergies
{
    public class StreamingEnergySpawner : Spawner<StreamingEnergy>, ISkillInstance
    {
        private readonly float _chance;

        public StreamingEnergySpawner(StreamingEnergySkill skill, CancellationToken token)
        {
            _chance = skill.Chance;
            Prefab = skill.Prefab;
            Pool = new StreamingEnergyPool(Prefab, StartAmount);

            MessageBrokerHolder.Enemy
                .Receive<M_EnemyDeath>()
                .Subscribe(message => HandleEnemyDeath(message.Position))
                .AddTo(token);
        }

        public void Disable() { }

        private void HandleEnemyDeath(Vector2 position)
        {
            if (Random.value > _chance)
                return;

            StreamingEnergy zone = Spawn();
            zone.transform.position = position;
        }
    }
}