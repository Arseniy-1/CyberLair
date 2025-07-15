using Project.Scripts.Stats;

namespace Project.Scripts.Interfaces
{
    public interface IWeaponStats
    {
        public WeaponDamage WeaponDamage { get; }
        public BulletPerShootCount BulletPerShootCount { get; }
        public WeaponSpread WeaponSpread { get; }
        public WeaponBulletReloadTime WeaponBulletReloadTime { get; }
    }
}