using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour, IDestoyable<FireZone>
{
    [SerializeField] private int _damagePerIteration = 2;
    [SerializeField] private float _burnInterval = 1f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _lifeTime = 10f;

    private List<IDamagable> _damagableTargets = new List<IDamagable>();
    private float _currentTime = 0f;

    private Coroutine _waitingDestroy;

    public event Action<FireZone> OnDestroyed;

    private void OnEnable()
    {
        if (_waitingDestroy != null)
            _waitingDestroy = StartCoroutine(WaitingDestroy());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable) & (_targetLayer << other.gameObject.layer) != 0)
        {
            _damagableTargets.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagable damagable) & (_targetLayer << other.gameObject.layer) != 0)
        {
            _damagableTargets.Remove(damagable);
        }
    }

    private void FixedUpdate()
    {
        if (Time.time >= _currentTime)
        {
            ApplyFireDamage();
            _currentTime = Time.time + _burnInterval;
        }
    }

    private void ApplyFireDamage()
    {
        foreach (IDamagable damagable in _damagableTargets)
        {
            damagable.TakeDamage(_damagePerIteration);
            Debug.Log("Нанес урон огнем ^_^");
        }
    }

    private IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        OnDestroyed.Invoke(this);
    }
}