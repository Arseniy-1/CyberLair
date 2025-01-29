using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public bool IsReloaded => _isReloaded;
    public bool Bullet => _bulletPrefab;
    public event Action<Bullet> OnShooted;

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
        if (_currentTime < _weaponStats.WeaponBulletReloadTime && !_isReloaded)
            _currentTime += Time.deltaTime;

        if (_currentTime >= _weaponStats.WeaponBulletReloadTime)
            Reload();
    }

    public abstract bool TryAttack();

    protected virtual void Reload()
    {
        _currentTime = 0;
        _isReloaded = true;
    }

    protected virtual void Attack()
    {
        Bullet bullet = _ammoSpawner.Spawn();
        bullet.Init(_shootPoint.position, GetBulletDirection(), _weaponStats.WeaponDamage);

        OnShooted?.Invoke(bullet);

        bullet.Activate();
    }

    protected virtual Quaternion GetBulletDirection()
    {
        Quaternion rotation = transform.rotation;
        rotation.z += Random.Range(-_weaponStats.WeaponSpread, _weaponStats.WeaponSpread);
        return rotation;
    }

    public void ApplyEffector(BulletEffector bulletEffector)
    {
        _bulletEffectors.Add(bulletEffector);
        bulletEffector.Initialize(this);
    }
}