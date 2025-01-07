using System;
using UnityEngine;
using UnityEngine.UI;
using Image = Microsoft.Unity.VisualStudio.Editor.Image;

[Serializable]
public class SkillInfo
{
    [field: SerializeField] public string SkillName {get; private set;}    
    [field: SerializeField] public string Description {get; private set;}    
    [field: SerializeField] public Sprite Icon {get; private set;}
    
}