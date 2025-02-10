using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Regeneration
{
    [CreateAssetMenu(fileName = "RegenerationSkill", menuName = "Skill/Simple/Regeneration", order = 51)]
    public class RegenerationSkill : Skill
    {
        [SerializeField] private StatModifier _regenerationModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.RegenerateAmount.AddModifier(_regenerationModifier.Copy());
        }
    }
}