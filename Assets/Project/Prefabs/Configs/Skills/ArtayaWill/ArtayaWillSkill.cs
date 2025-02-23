using UnityEngine;

[CreateAssetMenu(fileName = "ArtayaWillSkill", menuName = "Skill/Simple/ArtayaWill", order = 51)]
public class ArtayaWillSkill : Skill
{
    [SerializeField] private float _maxHealth = 1;
    [SerializeField] private float _shieldMultiplier = 3;
    
    public void Apply(SkillData skillData)
    {
        skillData.PlayerStats.Health.SetMaxHealth(_maxHealth);

        float newMaxShield = skillData.PlayerStats.ShieldAmount.CurrentValue * _shieldMultiplier;
        skillData.PlayerStats.ShieldAmount.SetMaxShield(newMaxShield);
    }
}