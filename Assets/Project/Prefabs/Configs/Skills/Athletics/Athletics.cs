using Project.Prefabs.Configs.Skills.Durability;

namespace Project.Prefabs.Configs.Skills.Athletics
{
    public class Athletics : ISkillInstance
    {
        private readonly StatModifier _jumpDistanceModifier;
        private readonly StatModifier _magnetRangeModifier;
        
        private readonly SkillData _skillData;
        
        public Athletics(SkillData skillData, AthleticsSkill skill)
        {
            _jumpDistanceModifier = skill.JumpDistanceModifier.Copy();
            _magnetRangeModifier = skill.MagnetRangeModifier.Copy();
            
            _skillData = skillData;
            
            skillData.PlayerStats.JumpDistance.AddModifier(_jumpDistanceModifier);
            skillData.PlayerStats.MagnetRange.AddModifier(_magnetRangeModifier);
        }

        public void Disable()
        {
            _skillData.PlayerStats.JumpDistance.RemoveModifier(_jumpDistanceModifier);
            _skillData.PlayerStats.MagnetRange.RemoveModifier(_magnetRangeModifier);
        }
    }
}