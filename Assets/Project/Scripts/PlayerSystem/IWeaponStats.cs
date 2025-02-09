public interface IWeaponStats
{
    WeaponDamage WeaponDamage { get; }
    BulletPerShootCount BulletPerShootCount { get; }
    WeaponSpread WeaponSpread { get; }
    WeaponBulletReloadTime WeaponBulletReloadTime { get; }
}