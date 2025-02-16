using UnityEngine;

[CreateAssetMenu(fileName = "RecoveryPainSkill", menuName = "Skill/Simple/RecoveryPain", order = 51)]
public class RecoveryPainSkill : Skill
{
    [SerializeField] private PainHealler _painHealer;
    
    public override void Apply(SkillData skillData)
    {
        var painHealler = Instantiate(_painHealer, skillData.WeaponHolder.transform);
        painHealler.Initialize(skillData);
    }
}