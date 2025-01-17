using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField, Range(0.01f, 20)] private float _reloadTime;
        [SerializeField] private int _damage;
        [SerializeField, Range(0, 1)] private float _spread;
    
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Animator _weaponAnimator;

        [SerializeField] private AmmoSpawner _ammoSpawner;
        [SerializeField] private List<BulletEffector> _bulletEffectors;
    
        private float _currentTime = 0;

        public event Action<Bullet> OnShooted;
    
        public bool IsReloaded { get; private set; }

        private void FixedUpdate()
        {
            if (_currentTime < _reloadTime && IsReloaded == false)
                _currentTime += Time.deltaTime;

            if (_currentTime >= _reloadTime)
                Reload();
        }

        [Button]
        public void TryAttack()
        {
            if (IsReloaded == false)
                return;

            Attack();

            IsReloaded = false;
        }

        public void ApplyStats(int damage, float spread, float reloadTime)
        {
            _damage = damage;
            _spread = spread;
            _reloadTime = reloadTime;
        }
    
        public void ApplyEffector(BulletEffector bulletEffector)
        {
            _bulletEffectors.Add(bulletEffector);
            bulletEffector.Initialize(this);
        }
    
        private void Attack()
        {
            Bullet bullet = _ammoSpawner.Spawn();
            bullet.Init(_shootPoint.transform.position, GetBulletDirection(), _damage);
        
            OnShooted?.Invoke(bullet);

            bullet.Activate();
        }

        private Quaternion GetBulletDirection()
        {
            Quaternion rotation = transform.rotation;

            rotation.z += Random.Range(-_spread, _spread);

            return rotation;
        }

        private void Reload()
        {
            _currentTime = 0;
            IsReloaded = true;
        }
   
        private void ShowAttackAnimation()
        {
            int attackAnim = Animator.StringToHash("Attack"); //TODO: �������
            _weaponAnimator.Play(attackAnim);
        }
    }
}