using UnityEngine;

[CreateAssetMenu(fileName = "New Summon Skill", menuName = "Skill/Active/Summon", order = 51)]
public class SummonSkill : ActiveSkill
{
    [SerializeField] private Summon _summonPrefab;
    
    [SerializeField, Header("Configs")] private SkillConfig _damageSkillConfig;
    [SerializeField] private SkillConfig _speedSkillConfig;
    [SerializeField] private SkillConfig _realoadSkillConfig;
    [SerializeField] private SkillConfig _spreadSkillConfig;

    [SerializeField, Header("Final Weapon")] private Weapon _finalWeaponPrefab;

    private Summon _summon;

    public override void Apply(WeaponHolder weaponHolder, int level)
    {
        if (!_summon)
        {
            _summon = Instantiate(_summonPrefab);
            _summon.Initialize(weaponHolder.transform);
        }
        
        if(level == MaxLevel)
            _summon.ApplyWeapon(_finalWeaponPrefab);

        var speed = _speedSkillConfig.Multipliers[level - 1];
        var damage = (int)_damageSkillConfig.Multipliers[level - 1];
        var reload = (int)_realoadSkillConfig.Multipliers[level - 1];
        var spread = (int)_spreadSkillConfig.Multipliers[level - 1];
            
        _summon.ApplyStats(speed, damage, reload, spread);
    }
}