using UnityEngine;

[CreateAssetMenu(fileName = "HeathSkill", menuName = "Skill/Simple/Health", order = 51)]
public class HealthSkill : Skill
{
    [SerializeField] private StatModifier _healthModifier;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.Health.AddModifier(_healthModifier);
    }
}