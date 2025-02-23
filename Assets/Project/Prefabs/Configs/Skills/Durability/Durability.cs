namespace Project.Prefabs.Configs.Skills.Durability
{
    public class Durability : SkillInstance
    {
        private readonly SkillData _data;

        private readonly StatModifier _healthModifier;
        private readonly StatModifier _regenerateModifier;
        
        public Durability(SkillData skillData, DurabilitySkill skill)
        {
            _data = skillData;

            _healthModifier = skill.HealthModifier.Copy();
            _regenerateModifier = skill.RegenerationModifier.Copy();
            
            _data.PlayerStats.Health.AddModifier(skill.HealthModifier.Copy());
            _data.PlayerStats.HealthRegenerateAmount.AddModifier(skill.RegenerationModifier.Copy());
        }

        public override void Disable()
        {
            _data.PlayerStats.Health.RemoveModifier(_healthModifier);
            _data.PlayerStats.HealthRegenerateAmount.RemoveModifier(_regenerateModifier);
        }
    }
}