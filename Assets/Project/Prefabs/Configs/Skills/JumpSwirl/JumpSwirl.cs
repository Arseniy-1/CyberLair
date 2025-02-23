using Project.Prefabs.Configs.Skills.Durability;

namespace Project.Prefabs.Configs.Skills.JumpSwirl
{
    public class JumpSwirl : SkillInstance
    {
        private StatModifier _jumpDistanceModifier;
        private StatModifier _magnetRangeModifier;
        
        private SkillData _skillData;
        
        public JumpSwirl(SkillData skillData, JumpSwirlSkill skill)
        {
            _jumpDistanceModifier = skill.JumpDistanceModifier.Copy();
            _magnetRangeModifier = skill.MagnetRangeModifier.Copy();
            
            _skillData = skillData;
            
            skillData.PlayerStats.JumpDistance.AddModifier(_jumpDistanceModifier);
            skillData.PlayerStats.MagnetRange.AddModifier(_magnetRangeModifier);
        }

        public override void Disable()
        {
            _skillData.PlayerStats.JumpDistance.RemoveModifier(_jumpDistanceModifier);
            _skillData.PlayerStats.MagnetRange.RemoveModifier(_magnetRangeModifier);
        }
    }
}