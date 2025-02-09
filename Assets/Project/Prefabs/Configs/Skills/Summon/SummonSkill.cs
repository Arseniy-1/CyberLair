using Project.Scripts.Weapon;
using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Skill", menuName = "Skill/Active/Summon", order = 51)]
public class SummonSkill : Skill
{
    [SerializeField] private Summon _summonPrefab;

    [SerializeField, Header("Configs")] private SkillConfig _damageSkillConfig;
    [SerializeField] private SkillConfig _speedSkillConfig;
    [SerializeField] private SkillConfig _realoadSkillConfig;
    [SerializeField] private SkillConfig _spreadSkillConfig;

    [SerializeField, Header("Final Weapon")]
    private Weapon _finalWeaponPrefab;

    private Summon _summon;

    public override void Apply(SkillData skillData)
    {
    }
}