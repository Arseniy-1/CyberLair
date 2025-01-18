using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Thunder Skill", menuName = "Skill/Active/Thunder", order = 0)]
    public class ThunderSkill : ActiveSkill
    {
        [SerializeField] private Thunder _thunderPrefab;
        
        [SerializeField] private SkillConfig _delayConfig;
        [SerializeField] private SkillConfig _radiusConfig;
        [SerializeField] private SkillConfig _damageConfig;
        [SerializeField] private SkillConfig _countConfig;
        
        private Thunder _thunder;
        
        public override void Apply(WeaponHolder weaponHolder, int level)
        {
            if (level > MaxLevel || level < 1)
                return;
            
            if (_thunder)
            {
                var delay = _delayConfig.Multipliers[level - 1];
                var radius = _radiusConfig.Multipliers[level - 1];
                var damage = _damageConfig.Multipliers[level - 1];
                var count = _countConfig.Multipliers[level - 1];
                _thunder.ApplyStats(delay, radius, damage, count);

                return;
            }
            
            _thunder = Instantiate(_thunderPrefab, weaponHolder.transform);
        }
    }
}