using UnityEngine;
using UnityEngine.Serialization;

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
        _skillData.PlayerStats.RegenerateAmount.AddModifier(_regenerateModifier.Copy());
    }
}