using System.Collections;
using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.SkillSystem.SkillViews.SummonSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SummonMover : MonoBehaviour
    {
        private const float MinDistanceToTarget = 1f;
        
        private readonly int _walkAnimation = Animator.StringToHash("Walk");
        
        [SerializeField] private Animator _animator;
    
        private ISummonMoveStats _summonStats;
        private Rigidbody2D _rigidbody;
        private Transform _selfTransform;
        private Vector2 _targetMovePosition;
        private Transform _targetTransform;
    
        private Vector2 SelfPosition => _selfTransform.position;
        private Vector2 TargetPosition => _targetTransform.position;
        private Vector2 RandomPointAroundTarget =>
            TargetPosition + Random.insideUnitCircle.normalized * _summonStats.MoveRadius;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _selfTransform = transform;
        }

        public void Initialize(Transform targetTransform, SummonStats summonStats)
        {
            _targetTransform = targetTransform;
            _summonStats = summonStats;
        
            StartCoroutine(ChangePosition());
        }
    
        public void MoveToNextPosition()
        {
            _animator.SetBool(
                _walkAnimation, 
                Vector2.Distance(SelfPosition, _targetMovePosition) < MinDistanceToTarget == false
                );

            if (Vector2.Distance(SelfPosition, _targetMovePosition) < MinDistanceToTarget)
            {
                _rigidbody.velocity = Vector2.zero;
                
                return;
            }

            var direction = (_targetMovePosition - SelfPosition).normalized;
            
            _rigidbody.velocity = direction * _summonStats.Speed.CurrentValue;
        }
    
        private IEnumerator ChangePosition()
        {
            var wait = new WaitForSeconds(_summonStats.MoveDelay);
        
            while (isActiveAndEnabled)
            {
                _targetMovePosition = RandomPointAroundTarget;

                yield return wait;
            }
        }
    }
}