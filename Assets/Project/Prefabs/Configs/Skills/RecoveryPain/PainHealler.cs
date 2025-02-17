using UnityEngine;

public class PainHealler : MonoBehaviour
{
    [SerializeField] private StatModifier _regenerateModifier;
    
    private SkillData _skillData;
    
    public void Initialize(SkillData skillData)
    {
        _skillData = skillData;
        skillData.PlayerStats.Health.DamageTaken += Heal;
    }

    private void Heal(float amount)
    {
        _skillData.PlayerStats.HealthRegenerateAmount.AddModifier(_regenerateModifier.Copy());
    }
}