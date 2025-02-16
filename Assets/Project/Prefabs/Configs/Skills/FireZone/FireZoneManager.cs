using System;
using Project.Scripts.Weapon;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.FireZone
{
    [Serializable]
    public class FireZoneManager
    {
        [SerializeField] private float _chance;
        [SerializeField] private FireZone _fireZonePrefab;
        [SerializeField] private FireZoneSpawner _fireZoneSpawner;
        
        public void Initialize(Weapon weapon)
        {
            _fireZoneSpawner = new FireZoneSpawner(_fireZonePrefab);
            weapon.Shooted += OnShot;
        }

        private void OnShot(Bullet bullet)
        {
            bullet.OnDestroyed += Explode;
        }

        private void Explode(Bullet bullet)
        {
            bullet.OnDestroyed -= Explode;

            var fireZone = _fireZoneSpawner.Spawn();
            fireZone.transform.position = bullet.transform.position;
        }
    }
}