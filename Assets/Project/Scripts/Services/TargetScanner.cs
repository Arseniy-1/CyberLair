using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class TargetScanner : MonoBehaviour
{
    [SerializeField] private float _scanRadius = 150f;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _scanDelay = 1;

    private WaitForSeconds _delay;

    public ITarget ClosestTarget { get; private set; }
    public bool HasTarget => ClosestTarget != null;

    private void Start()
    {
        _delay = new WaitForSeconds(_scanDelay);
        StartCoroutine(Scaning());
    }
    
    private IEnumerator Scaning()
    {
        while (enabled)
        {
            yield return _delay;
            Scan();
        }
    }

    public void Scan()
    {
        Vector2 position = transform.position;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _scanRadius, _targetLayer);
        HashSet<ITarget> targets = new HashSet<ITarget>();

        foreach (Collider2D hit in hits)
            if (hit.TryGetComponent(out ITarget target))
                targets.Add(target);

        List<ITarget> sortedTargets = targets.OrderBy(target => (target.Position - position).magnitude).ToList();

        if (sortedTargets.Count > 0)
        {
            ClosestTarget = sortedTargets.ToArray()[0];
        }
        else
        {
            ClosestTarget = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _scanRadius);
    }
}
