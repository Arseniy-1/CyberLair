using UniRx;
using UnityEngine;

public class StreamingEnergySpawner : Spawner<StreamingEnergy>, ISkillInstance
{
    private readonly float _chance;

    private readonly CompositeDisposable _disposable;

    public StreamingEnergySpawner(StreamingEnergySkill skill)
    {
        _chance = skill.Chance;
        Prefab = skill.Prefab;
        Pool = new StreamingEnergyPool(Prefab, StartAmount);

        _disposable = new CompositeDisposable();
        MessageBrokerHolder.Enemy.Receive<M_EnemyDeath>().Subscribe((message) => HandleEnemyDeath(message.Position))
            .AddTo(_disposable);
    }

    public void Disable()
    {
        _disposable?.Dispose();
    }

    private void HandleEnemyDeath(Vector2 position)
    {
        if (Random.value > _chance)
            return;

        var zone = Spawn();
        zone.transform.position = position;
    }
}