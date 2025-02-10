using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Multishot
{
    [CreateAssetMenu(fileName = "MultishotSkill", menuName = "Skill/Simple/Multishot", order = 51)]
    public class MultishotSkill : Skill
    {
        [SerializeField] private StatModifier _shotModifier;
        
        public override void Apply(SkillData skillData)
        {
            skillData.PlayerStats.BulletPerShootCount.AddModifier(_shotModifier.Copy());
        }
    }
}