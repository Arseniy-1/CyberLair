using UnityEngine;

namespace Project.Prefabs.Configs.Skills.JumpSwirl
{
    [CreateAssetMenu(fileName = "JumpSwirlSkill", menuName = "Skill/Hard/JumpSwirl", order = 51)]
    public class JumpSwirlSkill : HardSkill
    {
        [SerializeField] private StatModifier _jumpDistanceModifier;
        [SerializeField] private StatModifier _magnetRangeModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.JumpDistance.AddModifier(_jumpDistanceModifier.Copy());
            skillData.PlayerStats.MagnetRange.AddModifier(_magnetRangeModifier.Copy());
        }
    }
}