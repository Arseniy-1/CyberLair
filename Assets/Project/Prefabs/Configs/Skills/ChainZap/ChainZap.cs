using System.Collections.Generic;
using Project.Scripts.EnemySystem;
using UnityEngine;
using DG.Tweening;
using Project.Prefabs.Configs.Skills.Durability;
using Project.Prefabs.Configs.Skills.Zap;
using Project.Scripts.Weapon;
using Random = UnityEngine.Random;

public class ChainZap : SkillInstance
{
    private float _chainRadius;
    private int _maxBounces;
    private float _damageFalloff;
    private ChainZapView _zapView;
    private LayerMask _enemyLayer;
    private int _segments;
    private float _chance;
    private StatModifier _enemySpeedModifier;

    private Weapon _weapon;
    private ChainZapViewSpawner _viewSpawner;

    public ChainZap(SkillData skillData, ChainZapSkill skill)
    {
        _chainRadius = skill.ChainRadius;
        _maxBounces = skill.MaxBounces;
        _damageFalloff = skill.DamageFalloff;
        _zapView = skill.ZapView;
        _enemyLayer = skill.EnemyLayer;
        _segments = skill.Segments;
        _chance = skill.Chance;
        _enemySpeedModifier = skill.EnemySpeedModifier;
        
        _weapon = skillData.WeaponHolder.Weapon;
        _weapon.Shooted += InnerSubscribe;

        _viewSpawner = new ChainZapViewSpawner(_zapView, 0);
    }   

    public override void Disable()
    {
        _weapon.Shooted -= InnerSubscribe;
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

        for (int bounce = 0; bounce <= _maxBounces && currentTarget; bounce++)
        {
            hitTargets.Add(currentTarget);

            currentTarget.TakeDamage(_weapon.WeaponStats.WeaponDamage.CurrentValue * Mathf.Pow(_damageFalloff, bounce));
            currentTarget.EnemyStats.Speed.AddModifier(_enemySpeedModifier);

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
        ChainZapView view = _viewSpawner.Spawn();
        view.transform.position = bullet.transform.position;
        view.ZapView.positionCount = _segments;

        for (int i = 0; i < _segments; i++)
        {
            float t = i / (float)(_segments - 1);
            Vector2 point = Vector2.Lerp(start, end, t);
            float offset = Random.Range(-0.2f, 0.2f);
            
            point += Vector2.Perpendicular(end - start).normalized * offset;
            view.ZapView.SetPosition(i, point);
        }


        view.ZapView.material.mainTextureScale = new Vector2(Vector2.Distance(start, end), 1f);
        
        float duration = 0.2f;
        view.ZapView.material.DOFade(0f, duration).SetEase(Ease.InOutFlash).OnComplete(() =>
        {
            view.Disable();
            view.ZapView.material.DOFade(1f, 0f);
        });
    }
}