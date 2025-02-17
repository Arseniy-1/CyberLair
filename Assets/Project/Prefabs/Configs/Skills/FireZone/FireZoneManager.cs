using System;
using Project.Scripts.Weapon;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Prefabs.Configs.Skills.FireZone
{
    [Serializable]
    public class FireZoneManager
    {
        [SerializeField] private FireZone _fireZonePrefab;
        [SerializeField] private FireZoneSpawner _fireZoneSpawner;
        [SerializeField, Range(0f, 1f)] private float _chance;
        
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
            
            if(Random.value >= _chance)
                return;

            var fireZone = _fireZoneSpawner.Spawn();
            fireZone.transform.position = bullet.transform.position;
        }
    }
}