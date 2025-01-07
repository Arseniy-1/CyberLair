using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill/Active/FireSpheres", order = 51)]
public class FireSpheresSkill : ActiveSkill
{
    public override void Apply(WeaponHolder weaponHolder)
    {
        Weapon weapon = Instantiate(WeaponPrefab, weaponHolder.transform);
        weaponHolder.EquipWeapon(weapon);
    }
}