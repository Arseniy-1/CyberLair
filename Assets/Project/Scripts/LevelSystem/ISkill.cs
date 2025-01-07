using UnityEngine;

// public interface ISkill
// {
//     public SkillInfo SkillInfo { get;}
// }

public abstract class Skill : MonoBehaviour
{
    public SkillInfo SkillInfo { get;}
}

public class SoSkill : ScriptableObject
{
    public SkillInfo SkillInfo;
}

public class ASkill : SoSkill
{
    public Weapon WeaponPrefab;
}

public class PSkill : SoSkill
{
    public PassiveSkill PassiveSkillConfig;
}