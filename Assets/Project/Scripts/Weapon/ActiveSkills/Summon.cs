using UnityEngine;

public class Summon : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _spread;
    [SerializeField] private int _reloadTime;

    [SerializeField] private float _moveRadius = 5f;
    [SerializeField] private float _moveDelay = 2f;

    [SerializeField] private Weapon _weapon;

    [SerializeField] private WeaponHolder _weaponholder;
    [SerializeField] private TargetScanner _targetScanner;

    private Vector2 _currentMovePosition;
    private Transform _targetTransform;
    
    private Vector2 SelfPosition => transform.position;
    private Vector2 TargetPosition => _targetTransform.position;
    private Vector2 RandomPointAroundTarget => TargetPosition + Random.insideUnitCircle.normalized * _moveRadius;
    
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
        if (weapon == _weapon && !weapon)
            return;
        
        _weapon.gameObject.SetActive(false);

        _weapon = weapon;
    }

    private void MoveToNextPosition()
    {
        if (SelfPosition == _currentMovePosition)
            _currentMovePosition = RandomPointAroundTarget;

        transform.position = Vector2.MoveTowards(transform.position, _currentMovePosition, _speed * Time.deltaTime);
    }
}