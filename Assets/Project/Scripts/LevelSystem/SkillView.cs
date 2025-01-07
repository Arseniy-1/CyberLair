using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private Skill _skill;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }
    
    public event Action<Skill> OnClicked;

    private void HandleClick()
    {
        OnClicked?.Invoke(_skill);
    }
    
    public void SetSkill(Skill skill)
    {
        _skill = skill;
           // skill.SkillInfo
    }
}