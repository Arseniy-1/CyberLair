using System;
using UnityEngine;

namespace Project.Prefabs.Configs.Skills.MercuryMimicry
{
    [Serializable]
    public class MercuryMimicry
    {
        [SerializeField, ] private StatModifier _speedModifier;
        
        private SkillData _skillData;

        public void Initialize(SkillData skillData)
        {
            _skillData = skillData;
            skillData.PlayerStats.Health.DamageTaken += IncreaseSpeed;
        }

        private void IncreaseSpeed(float damage)
        {
            _skillData.PlayerStats.Speed.AddModifier(_speedModifier.Copy());
        }
    }
}