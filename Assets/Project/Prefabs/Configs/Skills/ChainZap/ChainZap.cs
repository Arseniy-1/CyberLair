using System;
using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;
using DG.Tweening;
using Project.Scripts.Weapon;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class ChainZap
{
    [SerializeField] private float _chainRadius = 5f;
    [SerializeField] private int _maxBounces = 2;
    [SerializeField] private float _damageFalloff = 0.8f;
    [SerializeField] private LineRenderer _zapView;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _segments = 20;
    [SerializeField, Range(0f, 1f)] private float _chance;

    private Weapon _weapon;

    public void Initialize(Weapon weapon)
    {
        _weapon = weapon;
        weapon.OnShot += InnerSubscribe;
    }

    private void InnerSubscribe(Bullet bullet)
    {
        bullet.OnDestroyed += CastLightning;
    }

    private void CastLightning(Bullet bullet)
    {
        bullet.OnDestroyed -= CastLightning;
        
        if (Random.value > _chance)
            return;

        List<Enemy> hitTargets = new List<Enemy>();
        Vector2 currentPosition = bullet.transform.position;
        Enemy currentTarget = FindClosestTarget(currentPosition, hitTargets);

        if (currentTarget == false)
            return;

        for (int bounce = 0; bounce < _maxBounces && currentTarget; bounce++)
        {
            hitTargets.Add(currentTarget);

            currentTarget.TakeDamage(_weapon.WeaponStats.WeaponDamage.CurrentValue * Mathf.Pow(_damageFalloff, bounce));

            if (bounce != 0)
                DrawLightning(currentPosition, currentTarget.transform.position, bullet);

            currentPosition = currentTarget.transform.position;
            currentTarget = FindClosestTarget(currentPosition, hitTargets);
        }
    }

    private Enemy FindClosestTarget(Vector3 position, List<Enemy> excludedTargets)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, _chainRadius, _enemyLayer);

        Enemy closestTarget = null;
        float closestDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy target) && !excludedTargets.Contains(target))
            {
                float distance = Vector2.Distance(position, target.transform.position);
                if (distance < closestDistance)
                {
                    closestTarget = target;
                    closestDistance = distance;
                }
            }
        }

        return closestTarget;
    }

    private void DrawLightning(Vector2 start, Vector2 end, Bullet bullet)
    {
        LineRenderer line = Object.Instantiate(_zapView, bullet.transform.position, Quaternion.identity);
        line.positionCount = _segments;

        for (int i = 0; i < _segments; i++)
        {
            float t = i / (float)(_segments - 1);
            Vector2 point = Vector2.Lerp(start, end, t);
            float offset = Random.Range(-0.2f, 0.2f);
            
            point += Vector2.Perpendicular(end - start).normalized * offset;
            line.SetPosition(i, point);
        }

        line.material.mainTextureScale = new Vector2(Vector2.Distance(start, end), 1f);

        float duration = 0.2f;
        line.material.DOFade(0f, duration).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            Object.Destroy(line.gameObject);
        });
    }
}