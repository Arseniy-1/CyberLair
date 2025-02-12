using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    [CreateAssetMenu(fileName = "ChainZapSkill", menuName = "Skill/Simple/ChainZap", order = 51)]
    public class FirstAidSkill : Skill
    {
        [SerializeField] private FirstAid _firstAid;
        
        public override void Apply(SkillData skillData)
        {
            _firstAid.Initialize(skillData.PlayerStats.Health);
        }
    }
}