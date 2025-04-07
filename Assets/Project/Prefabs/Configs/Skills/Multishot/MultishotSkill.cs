using UnityEngine;

    [CreateAssetMenu(fileName = "MultishotSkill", menuName = "Skill/Simple/Multishot", order = 51)]
    public class MultishotSkill : Skill
    {
        [field: SerializeField] public StatModifier BulletsPerShootModifier { get; private set; }
    }