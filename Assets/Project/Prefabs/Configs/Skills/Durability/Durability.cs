namespace Project.Prefabs.Configs.Skills.Durability
{
    public class Durability : SkillInstance
    {
        private readonly SkillData _data;
        private readonly DurabilitySkill _skill;
        
        public Durability(SkillData skillData, DurabilitySkill skill, SkillHolder skillHolder) : base(skillHolder)
        {
            _data = skillData;
            _skill = skill;
            
            _data.PlayerStats.Health.AddModifier(_skill.HealthModifier.Copy());
            _data.PlayerStats.HealthRegenerateAmount.AddModifier(_skill.RegenerationModifier.Copy());
        }

        public override void Disable()
        {
            _data.PlayerStats.Health.RemoveModifier(_skill.HealthModifier.Copy());
            _data.PlayerStats.HealthRegenerateAmount.RemoveModifier(_skill.RegenerationModifier.Copy());
        }
    }
}