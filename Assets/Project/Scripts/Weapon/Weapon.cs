using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab; 
        
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Animator _weaponAnimator;

        [SerializeField] private AmmoSpawner _ammoSpawner;
        [SerializeField] private List<BulletEffector> _bulletEffectors;
        
        private float _currentTime = 0;

        private IWeaponStats _weaponStats;
        
        public event Action<Bullet> OnShooted;

        public bool IsReloaded { get; private set; }

        private void Awake()
        {
            _ammoSpawner = new AmmoSpawner(_bulletPrefab);
            
            foreach (var effector in _bulletEffectors)
            {
                effector.Initialize(this);
            }
        }

        private void FixedUpdate()
        {
            if (_currentTime < _weaponStats.WeaponBulletReloadTime && IsReloaded == false)
                _currentTime += Time.deltaTime;

            if (_currentTime >= _weaponStats.WeaponBulletReloadTime)
                Reload();
        }

        public void Initialize(IWeaponStats weaponStats)
        {
            _weaponStats = weaponStats;
        }
        
        [Button]
        public virtual bool TryAttack()
        {
            if (IsReloaded == false)
                return false;

            Attack();

            IsReloaded = false;
            
            return true;
        }

        public void ApplyEffector(BulletEffector bulletEffector)
        {
            _bulletEffectors.Add(bulletEffector);
            bulletEffector.Initialize(this);
        }

        private void Attack()
        {
            Bullet bullet = _ammoSpawner.Spawn();
            bullet.Init(_shootPoint.transform.position, GetBulletDirection(), _weaponStats.WeaponDamage);

            OnShooted?.Invoke(bullet);

            bullet.Activate();
        }

        private Quaternion GetBulletDirection()
        {
            Quaternion rotation = transform.rotation;

            rotation.z += Random.Range(-_weaponStats.WeaponSpread, _weaponStats.WeaponSpread);

            return rotation;
        }

        private void Reload()
        {
            _currentTime = 0;
            IsReloaded = true;
        }
    }
}