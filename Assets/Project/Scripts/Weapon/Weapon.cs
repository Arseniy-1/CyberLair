using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _shootSoundPlayer;
            
        [SerializeField] protected Bullet BulletPrefab;
        [SerializeField] protected Transform ShootPoint;
        [SerializeField] protected Animator WeaponAnimator;
        [SerializeField] protected AmmoSpawner AmmoSpawner;
        [SerializeField] protected List<BulletEffector> BulletEffectors;
        
        protected float CurrentTime = 0;
        protected bool IsReloaded;
        protected IWeaponStats _weaponStats;

        public IWeaponStats WeaponStats => _weaponStats;
        public event Action<Bullet> Shooted;

        protected virtual void Awake()
        {
            AmmoSpawner = new AmmoSpawner(BulletPrefab);

            foreach (var effector in BulletEffectors)
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
            if (CurrentTime < _weaponStats.WeaponBulletReloadTime.CurrentValue && !IsReloaded)
                CurrentTime += Time.deltaTime;

            if (CurrentTime >= _weaponStats.WeaponBulletReloadTime.CurrentValue)
                Reload();
        }

        public abstract bool TryAttack();

        private void Reload()
        {
            CurrentTime = 0;
            IsReloaded = true;
        }

        protected void Attack()
        {
            _shootSoundPlayer.Play();
            
            for (int i = 0; i < _weaponStats.BulletPerShootCount.CurrentValue; i++)
            {
                Bullet bullet = AmmoSpawner.Spawn();
                bullet.Init(ShootPoint.position, GetBulletDirection(), (int)_weaponStats.WeaponDamage.CurrentValue);

                Shooted?.Invoke(bullet);

                bullet.Activate();
            }
        }

        private Quaternion GetBulletDirection()
        {
            Quaternion rotation = transform.rotation;
            rotation.z += Random.Range(-_weaponStats.WeaponSpread.CurrentValue, _weaponStats.WeaponSpread.CurrentValue);
            
            return rotation;
        }

        public void ApplyEffector(BulletEffector bulletEffector)
        {
            BulletEffectors.Add(bulletEffector);
            bulletEffector.Initialize(this);
        }
    }
}