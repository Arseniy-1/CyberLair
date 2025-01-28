using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/Health", order = 51)]
public class HealthSkill : Skill
{
    [SerializeField] private SkillConfig _skillConfig;
    [SerializeField] private Shield _ShieldPrefab;
    
    private Shield _ShieldInstance;
    
    public override void Apply(SkillData skillData)
    {
        skillData.PlayerStats.Health.SetHealth((int)(skillData.StartPlayerStats.Health.MaxAmount * _skillConfig.Multipliers[skillData.Level]));

        if (skillData.Level == MaxLevel)
        {
            Instantiate(_ShieldPrefab, skillData.WeaponHolder.transform);
        }
    }
}