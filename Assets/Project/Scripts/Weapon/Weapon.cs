using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Bullet _bulletPrefab;
        [SerializeField] protected Transform _shootPoint;
        [SerializeField] protected Animator _weaponAnimator;
        [SerializeField] protected AmmoSpawner _ammoSpawner;
        [SerializeField] protected List<BulletEffector> _bulletEffectors;

        protected float _currentTime = 0;
        protected bool _isReloaded;
        protected IWeaponStats _weaponStats;

        public IWeaponStats WeaponStats => _weaponStats;
        public event Action<Bullet> Shooted;

        protected virtual void Awake()
        {
            _ammoSpawner = new AmmoSpawner(_bulletPrefab);

            foreach (var effector in _bulletEffectors)
            {
                effector.Initialize(this);
            }
        }

        public virtual void Initialize(IWeaponStats weaponStats)
        {
            _weaponStats = weaponStats;
        }

        protected virtual void FixedUpdate()
        {
            if (_currentTime < _weaponStats.WeaponBulletReloadTime.CurrentValue && !_isReloaded)
                _currentTime += Time.deltaTime;

            if (_currentTime >= _weaponStats.WeaponBulletReloadTime.CurrentValue)
                Reload();
        }

        public abstract bool TryAttack();

        protected void Reload()
        {
            _currentTime = 0;
            _isReloaded = true;
        }

        protected void Attack()
        {
            for (int i = 0; i < _weaponStats.BulletPerShootCount.CurrentValue; i++)
            {
                Bullet bullet = _ammoSpawner.Spawn();
                bullet.Init(_shootPoint.position, GetBulletDirection(), (int)_weaponStats.WeaponDamage.CurrentValue);

                Shooted?.Invoke(bullet);

                bullet.Activate();
            }
        }

        protected Quaternion GetBulletDirection()
        {
            Quaternion rotation = transform.rotation;
            rotation.z += Random.Range(-_weaponStats.WeaponSpread.CurrentValue, _weaponStats.WeaponSpread.CurrentValue);
            return rotation;
        }

        public void ApplyEffector(BulletEffector bulletEffector)
        {
            _bulletEffectors.Add(bulletEffector);
            bulletEffector.Initialize(this);
        }
    }
}