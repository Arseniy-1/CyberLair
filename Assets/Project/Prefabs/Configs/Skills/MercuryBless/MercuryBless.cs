namespace Project.Prefabs.Configs.Skills.MercuryBless
{
    public class MercuryBless : ISkillInstance
    {
        private readonly StatModifier _damageModifier;
        private readonly StatModifier _speedModifier;
        
        private readonly SkillData _skillData;

        public MercuryBless(SkillData skillData, MercuryBlessSkill skill)
        {
            _damageModifier = skill.DamageModifier.Copy();
            _speedModifier = skill.SpeedModifier.Copy();
            
            _skillData = skillData;
            
            _skillData.PlayerStats.WeaponDamage.AddModifier(_damageModifier);
            _skillData.PlayerStats.Speed.AddModifier(_speedModifier);
        }
        
        public void Disable()
        {
            _skillData.PlayerStats.WeaponDamage.RemoveModifier(_damageModifier);
            _skillData.PlayerStats.Speed.RemoveModifier(_speedModifier);
        }
    }
}