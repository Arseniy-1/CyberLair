using UnityEngine;

public abstract class ActiveSkill : Skill
{
    [SerializeField] protected Weapon WeaponPrefab;

    public abstract void Apply(WeaponHolder weaponHolder);
}