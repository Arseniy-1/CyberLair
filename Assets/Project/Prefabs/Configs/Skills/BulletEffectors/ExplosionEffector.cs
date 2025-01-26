using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.Scripts.Weapon;

[CreateAssetMenu(fileName = "New ExplosionEffector", menuName = "Skill/BulletEffectors/ExplosionEffector",order = 51)]
public class ExplosionEffector : BulletEffector
{
    [SerializeField] private float _range;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private int _explosionDamage;
    
    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += OnShooted;
    }

    private void OnShooted(Bullet bullet)
    {
        bullet.OnDestroyed += Explode;
    }

    private void Explode(Bullet bullet)
    {
        bullet.OnDestroyed -= Explode;

        List<Health> affected = Physics2D.OverlapCircleAll(bullet.transform.position, _range, _layerMask)
            .Select(hit =>
            {
                hit.TryGetComponent(out Health health);
                return health;
            }).Where(health => health).ToList();

        foreach (Health hit in affected)
        {
            hit.TakeDamage(_explosionDamage);
        }
    }
}