using UnityEngine;

[CreateAssetMenu(fileName = "HeathSkill", menuName = "Skill/Simple/Health", order = 51)]
public class HealthSkill : Skill
{
    [SerializeField] private SkillConfig _skillConfig;
    [SerializeField] private Shield _ShieldPrefab;
    
    private Shield _ShieldInstance;
    
    public override void Apply(SkillData skillData)
    {
    }
}