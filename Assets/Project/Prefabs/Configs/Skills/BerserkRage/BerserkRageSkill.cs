using UnityEngine;

[CreateAssetMenu(fileName = "BerserkRageSkill", menuName = "Skill/Simple/BerserkRage", order = 0)]
public class BerserkRageSkill : HardSkill
{
    private BerserkHealthRegenerator _berserkHealthRegenerator;
    
    public override void Apply(SkillData skillData)
    {
        _berserkHealthRegenerator.Initialize(skillData.PlayerStats.Health, skillData.PlayerStats.HealthRegenerateAmount);
    }
}