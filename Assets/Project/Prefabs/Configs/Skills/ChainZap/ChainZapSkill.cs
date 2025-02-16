using UnityEngine;

namespace Project.Prefabs.Configs.Skills.Zap
{
    [CreateAssetMenu(fileName = "ChainZapSkill", menuName = "Skill/Simple/ChainZap", order = 51)]
    public class ChainZapSkill : Skill
    {
        [field: SerializeField] public ChainZap ChainZap { get; private set; }
        
        public override void Apply(SkillData skillData)
        {
            ChainZap.Initialize(skillData.WeaponHolder.Weapon);
        }
    }
}