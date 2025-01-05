using UnityEngine;

public class PlayerStats 
{
    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime{ get; private set; }
    
    [field: SerializeField] public float Damage{ get; private set; }
    [field: SerializeField] public float ReloadTime{ get; private set; }
    [field: SerializeField] public Bullet BulletPrefab{ get; private set; }
}

// TODO: SO
public class PlayerConfig
{
    [field: SerializeField] public float JumpDistance { get; private set; }
    [field: SerializeField] public float JumpTime{ get; private set; }
    
    [field: SerializeField] public float Damage{ get; private set; }
    [field: SerializeField] public float ReloadTime{ get; private set; }
    [field: SerializeField] public Bullet BulletPrefab{ get; private set; }
}

public class PlayerStatsService
{
    private PlayerConfig _config;
    private PlayerStats _playerStats;

    public PlayerStats GetStats()
    {
        return _playerStats;
    }
    
    
}