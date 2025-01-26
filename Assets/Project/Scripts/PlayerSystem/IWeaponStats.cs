public interface IWeaponStats
{
    int WeaponDamage { get; }
    float WeaponSpread { get; }
    float WeaponBulletReloadTime { get; }
}

public interface IIncrementalWeaponStats : IWeaponStats
{
    float WeaponRechargingTime { get; }
    int WeaponMagazineSize { get; }
}