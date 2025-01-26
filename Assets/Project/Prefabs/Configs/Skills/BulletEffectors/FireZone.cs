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

    private List<IDamageable> _damagableTargets = new List<IDamageable>();
    private float _currentTime = 0f;

    private Coroutine _waitingDestroy;

    public event Action<FireZone> OnDestroyed;

    private void OnEnable()
    {
        _waitingDestroy = StartCoroutine(WaitingDestroy());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damagable) & (_targetLayer << collision.gameObject.layer) != 0)
        {
            _damagableTargets.Add(damagable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damagable) & (_targetLayer << collision.gameObject.layer) != 0)
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
        for (int i = 0; i < _damagableTargets.Count; i++)
        {
            _damagableTargets[i].TakeDamage(_damagePerIteration);
        }
    }

    private IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        OnDestroyed?.Invoke(this);
    }
}