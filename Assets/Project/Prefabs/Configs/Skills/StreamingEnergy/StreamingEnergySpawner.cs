using UniRx;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.StreamingEnergy
{
    public class StreamingEnergySpawner : Spawner<StreamingEnergy>, ISkillInstance
    {
        private readonly float _chance;
        
        private readonly CompositeDisposable _disposable;
        
        public StreamingEnergySpawner(StreamingEnergySkill skill)
        {
            _chance = skill.Chance;
            Prefab = skill.Prefab;
            
            _disposable = new CompositeDisposable();
            MessageBrokerHolder.Enemy.Receive<M_Enemy_Death>().Subscribe((message) => HandleEnemyDeath())
                .AddTo(_disposable);
        }
        
        public void Disable()
        {
            _disposable?.Dispose();
        }

        private void HandleEnemyDeath()
        {
            if (Random.value > _chance)
                return;

            Spawn();
        }
    }
}