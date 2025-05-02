using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Scripts.Servises
{
    [Serializable]
    public class Explosion
    {
        [SerializeField] private float _range;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private int _explosionDamage;

        public void Explode(Vector3 position)
        {
            List<IDamageable> affected = Physics2D.OverlapCircleAll(position, _range, _layerMask)
                .Select(hit =>
                {
                    hit.TryGetComponent(out IDamageable health);
                    return health;
                }).Where(health => health != null).ToList();

            foreach (IDamageable hit in affected)
            {
                hit.TakeDamage(_explosionDamage);
            }

            MessageBrokerHolder.Game.Publish(new M_Exploded(position));
        }
    }
}