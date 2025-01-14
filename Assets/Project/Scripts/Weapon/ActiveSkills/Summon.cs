using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Summon : MonoBehaviour
{
    [Header("Summon Settings")]
    [SerializeField] private Rigidbody2D _rigidbody;
    
    [SerializeField] private float _nominalSpeed;
    [SerializeField] private int _nominalDamage;
    [SerializeField, Range(0.01f, 20)] private float _nominalSpread;
    [SerializeField, Range(0.01f, 1)] private float _nominalReloadTime;

    [SerializeField] private float _moveRadius = 5f;
    [SerializeField] private float _moveDelay = 2f;

    [Header("Summon Weapon")]
    [SerializeField] private Weapon _weapon;

    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private TargetScanner _targetScanner;

    private Vector2 _currentMovePosition;
    [SerializeField ]private Transform _targetTransform;

    private float _currentSpeed;
    private int _currentDamage;
    private float _currentSpread;
    private float _currentReloadTime;

    private float _moveOffset = 0.5f;
    
    private Vector2 SelfPosition => transform.position;
    private Vector2 TargetPosition => _targetTransform.position;
    private Vector2 RandomPointAroundTarget => TargetPosition + Random.insideUnitCircle.normalized * _moveRadius;

    private void FixedUpdate()
    {
        ITarget target = _targetScanner.ClosestTarget;

        if (target != null)
        {
            _weaponHolder.SpotTarget(target);
            _weaponHolder.Shoot();
        }

        MoveToNextPosition();
    }
    
    public void Initialize(Transform targetTransform)
    {
        _targetTransform = targetTransform;
        
        StartCoroutine(ChangePosition());
    }

    public void ApplyStats(float speedMultiplier, float damageMultiplier, float reloadTimeMultiplier, float spreadMultiplier)
    {
        _currentSpeed = _nominalSpeed * speedMultiplier;
        _currentDamage = (int)(_nominalDamage * damageMultiplier);
        _currentSpread = _nominalSpread * spreadMultiplier;
        _currentReloadTime = _nominalReloadTime * reloadTimeMultiplier;

        _weapon.ApplyStats(_currentDamage, _currentSpread, _currentReloadTime);
    }

    public void ApplyWeapon(Weapon weapon)
    {
        if (weapon == _weapon && !weapon)
            return;
        
        _weapon.gameObject.SetActive(false);

        _weapon = weapon;
    }

    private void MoveToNextPosition()
    {
        if (Vector2.Distance(_currentMovePosition, SelfPosition) <= _moveOffset)
            return;
        
        Vector2 direction = (_currentMovePosition - SelfPosition).normalized;
        _rigidbody.MovePosition(_rigidbody.position + direction * (_currentSpeed * Time.fixedDeltaTime));
    }

    private IEnumerator ChangePosition()
    {
        var wait = new WaitForSeconds(_moveDelay);
        
        while (isActiveAndEnabled)
        {
            _currentMovePosition = RandomPointAroundTarget;

            yield return wait;
        }
    }
}