using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    public class GrenadeExploder : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _explosionDamage;

        private void OnEnable()
        {
            _weapon.OnShooted += InnerSubscribe;
        }

        private void OnDisable()
        {
            _weapon.OnShooted -= InnerSubscribe;
        }

        private void InnerSubscribe(Bullet bullet)
        {
            bullet.OnDestroyed += Explode;
        }

        private void Explode(Bullet bullet)
        {
            bullet.OnDestroyed -= Explode;

            List<Health> affected = Physics2D.OverlapCircleAll(bullet.transform.position, _range, _layerMask)
                .Select(collider =>
                {
                    collider.TryGetComponent(out Health health);
                    return health;
                }).Where(health => health).ToList();

            foreach (Health hit in affected)
            {
                hit.TakeDamage(_explosionDamage);
            }
        }
    }
}