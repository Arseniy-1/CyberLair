using UnityEngine;

public class SummonSkill : ActiveSkill
{
    [SerializeField] private Summon _summonPrefab;
    [SerializeField] private SkillConfig _damageSkillConfig;
    [SerializeField] private SkillConfig _speedSkillConfig;
    [SerializeField] private SkillConfig _realoadSkillConfig;
    [SerializeField] private SkillConfig _spreadSkillConfig;

    [SerializeField] private Weapon _finalWeaponPrefab;

    private Summon _summon;

    public override void Apply(WeaponHolder weaponHolder, int level)
    {
        if (!_summon)
        {
            _summon = Instantiate(_summonPrefab);
            _summon.Initialize(weaponHolder.transform);
        }
        else
        {
            // _summon.ApplyStats();
        }
    }
}