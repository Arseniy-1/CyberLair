using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Active/Shield", order = 51)]
public class ShieldSkill : ActiveSkill
{
    public override void Apply(WeaponHolder weaponHolder)
    {
        Weapon weapon = Instantiate(WeaponPrefab, weaponHolder.transform);
        weaponHolder.EquipWeapon(weapon);
    }
}