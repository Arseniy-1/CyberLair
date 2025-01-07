using System;
using UnityEngine;
using UnityEngine.UI;
using Image = Microsoft.Unity.VisualStudio.Editor.Image;

[Serializable]
public class SkillInfo
{
    [SerializeField] private string _skillName;    
    [SerializeField] private string _description;    
    [SerializeField] private Sprite _icon;
    
}