using UnityEngine;

namespace Project.Prefabs.Configs.Skills
{
    [CreateAssetMenu(fileName = "FirstAidSkill", menuName = "Skill/Simple/FirstAid", order = 51)]
    public class FirstAidSkill : Skill
    {
        [SerializeField] private FirstAid _firstAid;
        
        public override void Apply(SkillData skillData)
        {
            _firstAid.Initialize(skillData.PlayerStats.Health);
        }
    }
}