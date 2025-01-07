using UnityEngine;

public class FireSpheresSkill : ActiveSkill
{
    public override void Apply(WeaponHolder weaponHolder)
    {
        Weapon weapon = Instantiate(WeaponPrefab, weaponHolder.transform);
        weaponHolder.EquipWeapon(weapon);
    }
}

