public interface IWeaponStats
{
    int WeaponDamage { get; }
    int BulletPerShootCount { get; }
    float WeaponSpread { get; }
    float WeaponBulletReloadTime { get; }
}