public interface IIncrementalWeaponStats : IWeaponStats
{
    float WeaponRechargingTime { get; }
    int WeaponMagazineSize { get; }
}