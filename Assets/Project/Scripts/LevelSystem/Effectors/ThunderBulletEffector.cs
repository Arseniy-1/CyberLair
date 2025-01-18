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
        
        List<Enemy> hitTargets = new List<Enemy>();
        Vector2 currentPosition = bullet.transform.position;
        Enemy currentTarget = FindClosestTarget(currentPosition, hitTargets);

        for (int bounce = 0; bounce < _maxBounces && currentTarget != null; bounce++)
        {
            Debug.Log("Bounce");
            // Добавляем цель в список пораженных
            hitTargets.Add(currentTarget);

            // Наносим урон
            int currentDamage = Mathf.RoundToInt(bullet.Damage * Mathf.Pow(_damageFalloff, bounce));
            currentTarget.TakeDamage(currentDamage);

            // Рисуем молнию
            DrawLightning(currentPosition, currentTarget.transform.position, bullet);

            // Переход к следующей цели
            currentPosition = currentTarget.transform.position;
            currentTarget = FindClosestTarget(currentPosition, hitTargets);
        }
    }

    private Enemy FindClosestTarget(Vector3 position, List<Enemy> excludedTargets)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(position, _chainRadius, _enemyLayer);
        HashSet<Enemy> targets = new HashSet<Enemy>();

        foreach (Collider2D hit in hits)
            if (hit.TryGetComponent(out Enemy target))
                targets.Add(target);

        List<Enemy> sortedTargets =
            targets.OrderBy(target => (target.transform.position - position).magnitude).ToList();

        Debug.Log(sortedTargets[0]);
        return sortedTargets[0];
    }

    private void DrawLightning(Vector2 start, Vector2 end, Bullet bullet)
    {
        LineRenderer line = Object.Instantiate(_linePrefab, bullet.transform);
        line.SetPosition(0, start);
        line.SetPosition(1, end);

        // Удаляем линию после небольшой задержки
        Object.Destroy(line.gameObject, 0.2f);
    }
}