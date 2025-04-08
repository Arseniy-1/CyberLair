using System;
using System.Collections;
using Project.Scripts.EnemySystem.Bosses;
using UnityEngine;

public class Bullet : MonoBehaviour, IDestoyable<Bullet>, IMoveable, IReturnable
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float LifeTime;
    [SerializeField] private int _damage = 0;

    private Rigidbody2D _rigidbody2D;
    private Coroutine _coroutine;
    private WaitForSeconds _waitLife;

    public event Action<Bullet> OnDestroyed;
    public event Action<IDamageable> OnDamagableCollided;

    public Rigidbody2D Rigidbody2D => _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _waitLife = new WaitForSeconds(LifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReturnToPool();
        
        if (collision.TryGetComponent(out IDamageable damagable))
        {
            OnDamagableCollided?.Invoke(damagable);
            damagable.TakeDamage(_damage);
        }

    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     ReturnToPool();
    //
    //     if (other.collider.TryGetComponent(out IDamageable damagable))
    //     {
    //         OnDamagableCollided?.Invoke(damagable);
    //         damagable.TakeDamage(_damage);
    //     }
    // }

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

    private IEnumerator WaitDestroy()
    {
        yield return _waitLife;

        ReturnToPool();
    }

    public void ReturnToPool()
    {
        OnDestroyed?.Invoke(this);
    }
}