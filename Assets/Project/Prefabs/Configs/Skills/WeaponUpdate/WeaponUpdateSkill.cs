using UnityEngine;

[CreateAssetMenu(fileName = "WeaponUpdateSkill", menuName = "Skill/Simple/UpdateWeapon", order = 51)]
public class WeaponUpdateSkill : Skill
{
    [field: SerializeField] public StatModifier DamageStatModifier { get; private set; }
}