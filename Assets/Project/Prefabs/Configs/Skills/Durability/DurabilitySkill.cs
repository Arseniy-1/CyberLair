using UnityEngine;

[CreateAssetMenu(fileName = "DurabilitySkill", menuName = "Skill/Simple/Durability", order = 51)]
public class DurabilitySkill : Skill
{
    [SerializeField] private StatModifier _healthModifier;
    [SerializeField] private StatModifier _regenerationModifier;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.Health.AddModifier(_healthModifier.Copy());
        skillData.PlayerStats.Health.RegenerateAmount.AddModifier(_regenerationModifier.Copy());
    }
}