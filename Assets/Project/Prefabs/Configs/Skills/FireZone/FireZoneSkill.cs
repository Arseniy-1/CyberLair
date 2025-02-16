using Project.Prefabs.Configs.Skills.FireZone;
using UnityEngine;

[CreateAssetMenu(fileName = "FireZoneSkill", menuName = "Skill/Hard/FireZone",order = 51)]
public class FireZoneSkill : HardSkill
{
    [SerializeField] private FireZoneManager _fireZoneManager;
    
    public override void Apply(SkillData skillData)
    {
        _fireZoneManager.Initialize(skillData.WeaponHolder.Weapon);
    }
}