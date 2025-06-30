using System;
using System.Collections;
using Project.Scripts.EnemySystem.Bosses;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IDestoyable<Bullet>, IMoveable, IReturnable
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float LifeTime;
    [SerializeField] private int _damage;
    [SerializeField] private TrailRenderer _trail;

    private Coroutine _coroutine;
    private WaitForSeconds _waitLife;

    public event Action<Bullet> OnDestroyed;
    public event Action<IDamageable> OnDamagableCollided;

    public Rigidbody2D Rigidbody2D { get; private set; }

    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        
        _waitLife = new WaitForSeconds(LifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReturnToPool();

        if (collision.TryGetComponent(out IDamageable damagable) == false) 
            return;
        
        OnDamagableCollided?.Invoke(damagable);
        
        damagable.TakeDamage(_damage);
    }
    
    private void OnDisable()
    {
        ReturnToPool();
    }
    
    public void Activate()
    {
        Rigidbody2D.velocity = transform.right * Speed;
    }

    public void Init(Vector3 startPosition, Quaternion rotation, int damage)
    {
        _trail.Clear();
        _trail.enabled = false;
        StartCoroutine(ReenableTrailNextFrame());

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
    
    private IEnumerator ReenableTrailNextFrame()
    {
        yield return null;
        
        _trail.enabled = true;
    }
}
