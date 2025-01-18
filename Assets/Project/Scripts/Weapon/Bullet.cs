using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour, IDestoyable<Bullet>
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float LifeTime;
    [SerializeField] private int _damage = 0;
    
    private Rigidbody2D _rigidbody2D;
    private Coroutine _coroutine;
    private WaitForSeconds _waitLife;

    public event Action<Bullet> OnDestroyed;

    public int Damage => _damage;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _waitLife = new WaitForSeconds(LifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(_damage);

            Destory();
        }
    }

    public void Activate()
    {
        _rigidbody2D.velocity = transform.right * Speed;
    }

    public void Init(Vector3 startPosition, Quaternion rotation, int damage)
    {
        _damage = damage;
        transform.position = startPosition;
        transform.rotation = rotation;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(WaitDestroy());
    }
    
    private void Destory()
    {
        OnDestroyed?.Invoke(this);
    }

    private IEnumerator WaitDestroy()
    {
        yield return _waitLife;
        Destory();
    }
}

public abstract class Destroyable<T> : MonoBehaviour
{
    public event Action<T> OnDestroyed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleCollision(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent(out IDamagable damagable))
        {

        }
    }
}
