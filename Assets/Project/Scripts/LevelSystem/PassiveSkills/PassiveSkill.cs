using UnityEngine;
using UnityEngine.Serialization;

public abstract class PassiveSkill : Skill
{
    [FormerlySerializedAs("PassiveSkillConfig")] [field: SerializeField] protected SkillConfig skillConfig;

    public abstract void Apply(PlayerStats playerStats, PlayerConfig playerConfig, int level);
}