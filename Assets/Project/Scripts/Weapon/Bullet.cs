using System;
using System.Collections;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Weapon
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour, IDestoyable<Bullet>, IMoveable, IReturnable
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected float LifeTime;
        [SerializeField] private int _damage;
        [SerializeField] private TrailRenderer _trail;

        private Coroutine _coroutine;
        private WaitForSeconds _waitLife;
        private Transform _transform;

        public event Action<Bullet> OnDestroyed;
        public event Action<IDamageable> OnDamagableCollided;

        public Rigidbody2D Rigidbody2D { get; private set; }
        public Vector3 Position => _transform.position;

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
            _transform = transform;
            
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

        public void Initialize(Vector3 startPosition, Quaternion rotation, int damage)
        {
            _damage = damage;
            _transform.position = startPosition;
            _transform.rotation = rotation;
            
            _trail.Clear();

            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitDestroy());
        }
        
        public void ReturnToPool()
        {
            OnDestroyed?.Invoke(this);
        }

        private IEnumerator WaitDestroy()
        {
            yield return _waitLife;

            ReturnToPool();
        }
    }
}
