using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Thunder Skill", menuName = "Skill/Active/Thunder", order = 0)]
    public class ThunderSkill : ActiveSkill
    {
        [SerializeField] private Thunder _thunderPrefab;
        [SerializeField] private float _radius;
        
        [SerializeField] private SkillConfig _radiusConfig;
        [SerializeField] private SkillConfig _damageConfig;
        [SerializeField] private SkillConfig _countConfig;
        
        private Thunder _thunderInstance;
        
        public override void Apply(WeaponHolder weaponHolder, int level)
        {
            if (_thunderInstance != null)
            {
                var radius = _radiusConfig.Multipliers[level];
                var damage = _damageConfig.Multipliers[level];
                var count = _countConfig.Multipliers[level];
                _thunderInstance.ApplyStats(radius, damage, count);
            }
            
            _thunderInstance = Instantiate(_thunderPrefab, weaponHolder.transform);
            _thunderInstance.Initialize(_radius, weaponHolder.transform);
        }
    }
}