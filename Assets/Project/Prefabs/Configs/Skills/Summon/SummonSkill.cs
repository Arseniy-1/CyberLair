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
        if (!_summon)
        {
            _summon = Instantiate(_summonPrefab);
            _summon.Initialize(skillData.WeaponHolder.transform);
        }

        if (skillData.Level == MaxLevel)
            _summon.ApplyWeapon(_finalWeaponPrefab);

        var speed = _speedSkillConfig.Multipliers[skillData.Level];
        var damage = (int)_damageSkillConfig.Multipliers[skillData.Level];
        var reload = (int)_realoadSkillConfig.Multipliers[skillData.Level];
        var spread = (int)_spreadSkillConfig.Multipliers[skillData.Level];

        _summon.ApplyStats(speed, damage, reload, spread);
    }
}