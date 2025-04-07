using Project.Scripts.EnemySystem;

public struct M_BossSpawned
{
    public M_BossSpawned(Enemy enemy)
    {
        Boss = enemy;
    }
    
    public Enemy Boss { get; private set; }
}