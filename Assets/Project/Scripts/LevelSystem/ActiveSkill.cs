using UnityEngine;

public abstract class ActiveSkill: ISkill
{
    public SkillInfo SkillInfo { get; }
    
    [SerializeField] protected Weapon WeaponPrefab;
    
    public abstract void Apply(WeaponHolder weaponHolder);
}

public class FireSpheresSkill : ActiveSkill
{
    public override void Apply(WeaponHolder weaponHolder)
    {
        Weapon weapon = Object.Instantiate(WeaponPrefab, weaponHolder.transform);
        weaponHolder.EquipWeapon(weapon);
    }
}