using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapon : MonoBehaviour
{
    [SerializeField, Range(0.01f, 20)] private float _reloadTime;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField, Range(0, 1)] private float _spread;

    [SerializeField] private Animator _weaponAnimator;

    [SerializeField] private AmmoSpawner _ammoSpawner;

    private float _currentTime = 0;

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

    private void Attack()
    {
        Bullet bullet = _ammoSpawner.Spawn();
        bullet.Init(_shootPoint.transform.position, GetBulletDirection());

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