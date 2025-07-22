using System;
using UnityEngine;

namespace Project.Scripts.SkillSystem
{
    [Serializable]
    public class SkillInfo
    {
        [field: SerializeField] public string SkillName { get; private set; }    
        [field: SerializeField] public string Description { get; private set; }    
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}