using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private Weapon.Weapon _currentWeapon;

        public Weapon.Weapon Weapon => _currentWeapon;
    
        public void Shoot()
        {
            if (_currentWeapon.TryAttack() == false)
                return;
        }

        public void SpotTarget(ITarget target)
        {
            Vector3 targetPosition = target.Position;
            var direction = targetPosition - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _currentWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}