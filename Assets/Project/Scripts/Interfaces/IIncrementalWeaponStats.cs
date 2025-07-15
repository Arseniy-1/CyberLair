using Project.Scripts.Stats;

namespace Project.Scripts.Interfaces
{
    public interface IIncrementalWeaponStats : IWeaponStats
    {
        public WeaponRechargingTime WeaponRechargingTime { get; }
        public WeaponMagazineSize WeaponMagazineSize { get; }
    }
}