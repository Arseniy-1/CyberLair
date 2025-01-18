using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.EnemySystem;
using Project.Scripts.Weapon;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "New ThunderBulletEffector", menuName = "Skill/BulletEffectors/ThunderBulletEffector", order = 51)]
public class ThunderBulletEffector : BulletEffector
{
    [SerializeField] private float _chainRadius = 5f; // Радиус поиска следующей цели
    [SerializeField] private int _maxBounces = 5; // Количество отскоков молнии
    [SerializeField] private float _damageFalloff = 0.8f; // Уменьшение урона с каждым отскоком
    [SerializeField] private LineRenderer _linePrefab; // Префаб LineRenderer для молнии
    [SerializeField] private LayerMask _enemyLayer; // Слой врагов

    private Enemy _lastHitedTarget;
    
    public override void Initialize(Weapon weapon)
    {
        Weapon = weapon;
        weapon.OnShooted += SubBulletDestroy;
    }

    private void SubBulletDestroy(Bullet bullet)
    {
        bullet.OnDestroyed += CastLightning;
    }

    private void CastLightning(Bullet bullet)
    {
        bullet.OnDestroyed -= CastLightning;
        
        List<Enemy> hitenTargets = new List<Enemy>();
        Vector2 currentPosition = bullet.transform.position;
        Enemy currentTarget;

        for (int bounce = 0; bounce < 5; bounce++)
        {
            currentTarget = FindClosestTarget(currentPosition, hitenTargets);
            Debug.Log("Bounce");
            
            hitenTargets.Add(currentTarget);

            // Наносим урон
            int currentDamage = Mathf.RoundToInt(bullet.Damage * Mathf.Pow(_damageFalloff, bounce));
            currentTarget.TakeDamage(currentDamage);

            if(_lastHitedTarget != null)
                DrawLightning(_lastHitedTarget.transform.position, currentTarget.transform.position, bullet);

            // Переход к следующей цели
            currentPosition = currentTarget.transform.position;

            _lastHitedTarget = currentTarget;
        }
        
        _lastHitedTarget = null;
    }

    private Enemy FindClosestTarget(Vector3 position, List<Enemy> excludedTargets)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, _chainRadius, _enemyLayer);
        HashSet<Enemy> targets = new HashSet<Enemy>();

        foreach (Collider2D hit in hits)
            if (hit.TryGetComponent(out Enemy target) && excludedTargets.Contains(target) == false)
                targets.Add(target);

        List<Enemy> sortedTargets =
            targets.OrderBy(target => (target.transform.position - position).magnitude).ToList();

        Debug.Log(sortedTargets[0]);
        return sortedTargets[0];
    }

    private void DrawLightning(Vector2 start, Vector2 end, Bullet bullet)
    {
        Debug.Log(start);
        Debug.Log(end);
        Gizmos.DrawSphere(bullet.transform.position, _chainRadius);
        LineRenderer line = Object.Instantiate(_linePrefab, bullet.transform.position, bullet.transform.rotation);
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // Удаляем линию после небольшой задержки
        Object.Destroy(line.gameObject, 5f);
    }
}