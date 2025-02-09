public interface IIncrementalWeaponStats : IWeaponStats
{
    WeaponRechargingTime WeaponRechargingTime { get; }
    WeaponMagazineSize WeaponMagazineSize { get; }
}