using System;

namespace Project.Prefabs.Configs.Skills.MercuryMimicry
{
    [Serializable]
    public class MercuryMimicry : ISkillInstance
    {
        private StatModifier _speedModifier;
        private SkillData _skillData;

        public MercuryMimicry(SkillData skillData, MercuryMimicrySkill skill)
        {
            _speedModifier = skill.SpeedModifier;
            _skillData = skillData;
            
            _skillData.PlayerStats.Health.DamageTaken += IncreaseSpeed;
        }

        private void IncreaseSpeed(float damage)
        {
            _skillData.PlayerStats.Speed.AddModifier(_speedModifier.Copy());
        }

        public void Disable()
        {
            _skillData.PlayerStats.Health.DamageTaken -= IncreaseSpeed;
        }
    }
}