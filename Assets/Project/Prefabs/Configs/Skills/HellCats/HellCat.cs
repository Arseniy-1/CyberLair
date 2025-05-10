using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HellCat : MonoBehaviour, IDestoyable<HellCat>
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private AudioSource _audioSource;
    
    [SerializeField, Header("Hell Cat Stats")] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _scanRadius = 150;
    
    private ITarget _target;
    
    public event Action<HellCat> OnDestroyed;

    private void OnEnable()
    {
        _audioSource.Play();
        
        FindTarget();
    }

    private void FixedUpdate()
    {
        if (_target == null)
            return;
        
        var newPosition = Vector2.MoveTowards(_rigidbody.position, _target.Position,
            _speed * Time.fixedDeltaTime);
        
        _rigidbody.MovePosition(newPosition);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out IDamageable damageable))
            damageable.TakeDamage(_damage);
        
        OnDestroyed?.Invoke(this);
    }
    
    private void FindTarget()
    {
        Vector2 position = transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _scanRadius, _targetLayer);
        HashSet<ITarget> targets = new HashSet<ITarget>();

        foreach (Collider2D hit in hits)
            if (hit.TryGetComponent(out ITarget target))
                targets.Add(target);

        List<ITarget> sortedTargets = targets.OrderBy(target => (target.Position - position).magnitude).ToList();

        _target = sortedTargets.Count > 0 ? sortedTargets.ToArray()[0] : null;
    }
}