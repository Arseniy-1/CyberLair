using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "New Thunder Skill", menuName = "Skill/Active/Thunder", order = 0)]
    public class ThunderSkill : Skill
    {
        [SerializeField] private Thunder _thunderPrefab;
        
        [SerializeField] private SkillConfig _delayConfig;
        [SerializeField] private SkillConfig _radiusConfig;
        [SerializeField] private SkillConfig _damageConfig;
        [SerializeField] private SkillConfig _countConfig;
        
        private Thunder _thunder;
        
        public override void Apply(SkillData skillData)
        {
            if (skillData.Level > MaxLevel || skillData.Level < 0)
                return;
            
            if(!_thunder)
                _thunder = Instantiate(_thunderPrefab, skillData.WeaponHolder.transform);

            var delay = _delayConfig.Multipliers[skillData.Level];
            var radius = _radiusConfig.Multipliers[skillData.Level];
            var damage = _damageConfig.Multipliers[skillData.Level];
            var count = _countConfig.Multipliers[skillData.Level];
            _thunder.ApplyStats(delay, radius, damage, count);
        }
    }
}