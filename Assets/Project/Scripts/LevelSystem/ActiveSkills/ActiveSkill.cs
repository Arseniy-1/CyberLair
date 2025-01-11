using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public abstract void Apply(WeaponHolder weaponHolder, int level);
}

public class SummonSkill : ActiveSkill
{
    [SerializeField] private Summon _summonPrefab;
    [SerializeField] private SkillConfig _damageSkillConfig;
    [SerializeField] private SkillConfig _speedSkillConfig;
    [SerializeField] private SkillConfig _realoadSkillConfig;
    [SerializeField] private SkillConfig _spreadSkillConfig;

    [SerializeField] private Weapon _finalWeaponPrefab;

    private Summon _summon;

    public override void Apply(WeaponHolder weaponHolder, int level)
    {
        if (!_summon)
        {
            _summon = Instantiate(_summonPrefab);
            _summon.Initialize(weaponHolder.transform);
        }
        else
        {
            _summon.ApplyStats();
        }
    }
}

public class Summon : MonoBehaviour
{
    private Transform _targetTransform;

    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _spread;
    [SerializeField] private int _reloadTime;

    [SerializeField] private float _moveRadius = 5f;
    [SerializeField] private float _moveDelay = 2f;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private WeaponHolder _weaponholder;
    [SerializeField] private TargetScanner _targetScanner;

    private Vector2 GetRandomPointOnCircleEdge => Random.insideUnitCircle.normalized * _moveRadius;
    
    public void Initialize(Transform targetTransform)
    {
        _targetTransform = targetTransform;
    }

    public void ApplyStats(float speed, int damage)
    {
        _speed = speed;
        _damage = damage;

        _weapon.ApplyStats(_damage, _spread, _reloadTime);
    }

    public void ApplyWeapon(Weapon weapon)
    {
    }

    private void FixedUpdate()
    {
        ITarget target = _targetScanner.ClosestTarget;

        if (target != null)
        {
            _weaponholder.SpotTarget(target);
            _weapon.TryAttack();
        }

        MoveToNextPosition();
    }

    private void MoveToNextPosition()
    {
        // Плавное перемещение объекта к следующей позиции
        Vector3 targetPosition = new Vector3(_nextPosition.x, transform.position.y, _nextPosition.y);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }
}