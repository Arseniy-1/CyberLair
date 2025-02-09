using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

namespace Project.Scripts.LevelSystem.ActiveSkills
{
    [CreateAssetMenu(fileName = "ThunderSkill", menuName = "Skill/Simple/Thunder", order = 0)]
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
        }
    }
}