using Project.Scripts.EnemySystem;

public struct BossSpawnedMessage
{
    public BossSpawnedMessage(Enemy enemy)
    {
        Boss = enemy;
    }
    
    public Enemy Boss { get; private set; }
}