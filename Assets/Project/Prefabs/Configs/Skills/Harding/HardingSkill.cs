using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Harding
{
    [CreateAssetMenu(fileName = "HardingSkill", menuName = "Skill/Simple/Harding", order = 51)]
    public class HardingSkill : Skill
    {
        [SerializeField] private StatModifier _jumpReloadTimeModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.JumpReloadTime.AddModifier(_jumpReloadTimeModifier);
        }
    }
}