using UnityEngine;

[CreateAssetMenu(fileName = "New Shield Skill", menuName = "Skill/Active/Shield", order = 51)]
public class ShieldSkill : ActiveSkill
{
    public override void Apply(WeaponHolder weaponHolder, int level)
    {
        // Instantiate(WeaponPrefab, weaponHolder.transform);
        
        // Weapon weapon = Instantiate(WeaponPrefab, weaponHolder.transform);
        // weaponHolder.EquipWeapon(weapon);
    }
}