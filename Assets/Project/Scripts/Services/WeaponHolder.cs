using Project.Scripts.Weapon;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private Weapon _currentWeapon;

    public Weapon Weapon => _currentWeapon;
    
    public void Shoot()
    {
        _currentWeapon.TryAttack();
    }

    public void SpotTarget(ITarget target)
    {
        Vector3 targetPosition = target.Position;
        var direction = targetPosition - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}