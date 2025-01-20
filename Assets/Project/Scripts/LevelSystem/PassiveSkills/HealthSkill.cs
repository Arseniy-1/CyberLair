using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Passive/Health", order = 51)]
public class HealthSkill : Skill
{
    [SerializeField] private SkillConfig _skillConfig;
    [SerializeField] private Shield _ShieldPrefab;
    
    private Shield _ShieldInstance;
    
    public override void Apply(SkillData skillData)//TODO: попробовать связать общим классом
    {
        skillData.PlayerStats.Health.IncreaseHealth((int)(skillData.PlayerConfig.Health.MaxValue *
                                                          _skillConfig.Multipliers[skillData.Level]) - skillData.PlayerConfig.Health.MaxValue);

        if (skillData.Level == MaxLevel)
        {
            Instantiate(_ShieldPrefab, skillData.WeaponHolder.transform);
        }
    }
}

public class Shield : MonoBehaviour
{
    
}