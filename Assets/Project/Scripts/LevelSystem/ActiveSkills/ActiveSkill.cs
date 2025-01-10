using Project.Scripts.Weapon.ActiveSkills;
using UnityEngine;

public abstract class ActiveSkill : Skill
{
    public abstract void Apply(WeaponHolder weaponHolder, int level);
}