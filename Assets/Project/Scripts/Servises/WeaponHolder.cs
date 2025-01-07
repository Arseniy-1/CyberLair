using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private TargetScanner _targetScaner;

    [SerializeField] private Weapon _currentWeapon;

    public event Action OnWeaponChanged;

    private void FixedUpdate()
    {
        SpotTarget();
    }

    [Button]
    public void Shoot()
    {
        _currentWeapon.TryAttack();
    }

    private void SpotTarget()
    {
        if (_targetScaner.HasTarget)
        {
            Vector3 targetPosition = _targetScaner.ClosestTarget.Position;
            var direction = targetPosition - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}