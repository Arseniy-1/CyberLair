using UnityEngine;

[CreateAssetMenu(fileName = "New CriticalHitEffector", menuName = "Skill/BulletEffectors/CriticalHitEffector",
    order = 51)]
public class CriticalHitEffector : BulletEffector
{
    [SerializeField] private int _criticalHitChance;

    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += OnShooted;
    }

    private void OnShooted(Bullet bullet)
    {
        bullet.OnDamagableCollided += HandleDamageable;
    }

    private void HandleDamageable(IDamageable damageable)
    {
        if (Random.Range(0, 100) < _criticalHitChance)
        {
            damageable.TakeDamage(Weapon.WeaponStats.WeaponDamage);
        }
    }
}