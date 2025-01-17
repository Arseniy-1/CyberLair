using Sirenix.OdinInspector;
using System;
using Project.Scripts.Weapon;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;

    public event Action OnWeaponChanged;

    [Button]
    public void Shoot()
    {
        _currentWeapon.TryAttack();
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = newWeapon;
    }

    [Button]
    public void SpotTarget(ITarget target)
    {
        Vector3 targetPosition = target.Position;
        var direction = targetPosition - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}