using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "New Shield Skill", menuName = "Skill/Active/Shield", order = 51)]
public class ShieldSkill : Skill
{
    public override void Apply(SkillData skillData)
    {
        // Instantiate(WeaponPrefab, weaponHolder.transform);
        
        // Weapon weapon = Instantiate(WeaponPrefab, weaponHolder.transform);
        // weaponHolder.EquipWeapon(weapon);
    }
}