using System;
using System.Collections;
using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;

public class StreamingEnergy : MonoBehaviour, IDestoyable<StreamingEnergy>
{
    [SerializeField] private float _stunDuration = 0.2f;
    [SerializeField] private StatModifier _speedModifier;
    [SerializeField] private float _stunInterval = 1.5f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _lifeTime = 3.5f;

    private readonly List<Enemy> _enemies = new();

    private Coroutine _waitingDestroy;
    private Coroutine _stunIterating;

    public event Action<StreamingEnergy> OnDestroyed;

    private void OnEnable()
    {
        _waitingDestroy = StartCoroutine(WaitingDestroy());
        _stunIterating = StartCoroutine(StunIterating());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _enemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(_waitingDestroy);
        StopCoroutine(_stunIterating);
    }

    private void ApplyStun()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (Enum.IsDefined(typeof(BossTypes), enemy.EnemyType))
                continue;
            
            enemy.TakeStun(_stunDuration);
            enemy.EnemyStats.Speed.AddModifier(_speedModifier.Copy());
        }
    }

    private IEnumerator StunIterating()
    {
        while (isActiveAndEnabled)
        {
            ApplyStun();
            
            yield return new WaitForSeconds(_stunInterval);
        }
    }

    private IEnumerator WaitingDestroy()
    {
        yield return new WaitForSeconds(_lifeTime);
        
        OnDestroyed?.Invoke(this);
    }
}