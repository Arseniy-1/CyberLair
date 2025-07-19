using Project.Scripts.EnemySystem;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts.SkillSystem.SkillViews.SummonSystem
{
    public class Summon : MonoBehaviour
    {
        [SerializeField] private WeaponHolder _weaponHolder;
        [SerializeField] private SummonMover _mover;
        [SerializeField] private SummonStats _summonStats;
        [SerializeField] private TargetScanner _targetScanner;
    
        private void FixedUpdate()
        {
            ITarget target = _targetScanner.ClosestTarget;

            if (target != null)
            {
                _weaponHolder.SpotTarget(target);
                _weaponHolder.Shoot();
            }

            _mover.MoveToNextPosition();
        }

        public void Initialize(Transform targetTransform)
        {
            _mover.Initialize(targetTransform, _summonStats);
            _summonStats.Initialize();
        
            _weaponHolder.Weapon.Initialize(_summonStats);
        }
    }
}