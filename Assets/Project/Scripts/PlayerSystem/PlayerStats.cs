using UnityEngine;

public class PlayerStats
{
    [field: SerializeField] public float Damage;

    [field: SerializeField] public float JumpDistance; 
    [field: SerializeField] public float JumpTime;

    [field: SerializeField] public float ReloadTime;
    [field: SerializeField] public Bullet BulletPrefab;

    public void Initialize(PlayerConfig playerConfig)
    {
        Damage = playerConfig.Damage;
        JumpDistance = playerConfig.JumpDistance;
        JumpTime = playerConfig.JumpTime;
        ReloadTime = playerConfig.ReloadTime;
        BulletPrefab = playerConfig.BulletPrefab;
    }
}