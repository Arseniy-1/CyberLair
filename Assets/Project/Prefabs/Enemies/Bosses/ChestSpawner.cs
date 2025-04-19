using Project.Scripts.EnemySystem;
using UniRx;
using UnityEngine;

public class ChestSpawner
{
    private readonly CompositeDisposable _disposable = new();
    
    private BossChest _bossChestPrefab;

    public ChestSpawner()
    {
        MessageBrokerHolder.Enemy.Receive<M_BossSpawned>().Subscribe((message) => HandleBossSpawn(message.Boss))
            .AddTo(_disposable);
    }

    private void HandleBossSpawn(Enemy enemy)
    {
        enemy.OnDestroyed += SpawnChest;
    }
    
    private void SpawnChest(Enemy enemy)
    {
        enemy.OnDestroyed -= SpawnChest;

        Object.Instantiate(_bossChestPrefab, enemy.transform.position, Quaternion.identity);
    }
}