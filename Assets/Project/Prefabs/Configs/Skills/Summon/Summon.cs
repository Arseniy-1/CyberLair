using Project.Scripts.Weapon;
using UnityEngine;

public class Summon : MonoBehaviour
{
    [Header("Summon Settings")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SummonMover _mover;
    
    [SerializeField] private int _nominalDamage;
    [SerializeField, Range(0.01f, 20)] private float _nominalSpread;
    [SerializeField, Range(0.01f, 1)] private float _nominalReloadTime;

    [Header("Summon Weapon")]
    [SerializeField] private Weapon _weapon;

    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private TargetScanner _targetScanner;

    private int _currentDamage;
    private float _currentSpread;
    private float _currentReloadTime;
    
    private void FixedUpdate()
    {
        ITarget target = _targetScanner.ClosestTarget;

        if (target != null)
        {
            _weaponHolder.SpotTarget(target);
            _weaponHolder.Shoot();
        }

        _mover.MoveToNextPosition();
    }
    
    public void Initialize(Transform targetTransform)
    {
        _mover.Initialize(targetTransform, _rigidbody);
    }

    public void ApplyStats(float speedMultiplier, float damageMultiplier, float reloadTimeMultiplier, float spreadMultiplier)
    {
        _currentDamage = (int)(_nominalDamage * damageMultiplier);
        _currentSpread = _nominalSpread * spreadMultiplier;
        _currentReloadTime = _nominalReloadTime * reloadTimeMultiplier;
        
        _mover.ApplyStats(speedMultiplier);
    }

    public void ApplyWeapon(Weapon weapon)
    {
        if (weapon == _weapon && !weapon)
            return;
        
        var currentWeapon = Instantiate(weapon, _weaponHolder.transform);
        _weaponHolder.EquipWeapon(currentWeapon);
    }
}