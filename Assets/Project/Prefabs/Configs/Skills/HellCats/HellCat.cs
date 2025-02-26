using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class HellCat : MonoBehaviour, IDestoyable<HellCat>
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private SummonMover _mover;
    
    private float _scanRadius = 150;
    private ITarget _target;
    
    public event Action<HellCat> OnDestroyed;

    private void OnEnable()
    {
        FindTarget();
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

        if (sortedTargets.Count > 0)
            _target = sortedTargets.ToArray()[0];
        else
            _target = null;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        OnDestroyed?.Invoke(this);
    }
}