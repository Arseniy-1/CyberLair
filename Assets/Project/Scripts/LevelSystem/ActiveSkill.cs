using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public SkillInfo SkillInfo { get; }

    [SerializeField] protected Weapon WeaponPrefab;

    public abstract void Apply(WeaponHolder weaponHolder);
}