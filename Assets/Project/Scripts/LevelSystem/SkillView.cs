using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Button _button;
    
    private ISkill _skill;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }
    
    public event Action<ISkill> OnClicked;

    private void HandleClick()
    {
        OnClicked?.Invoke(_skill);
    }
    
    public void SetSkill(ISkill skill)
    {
        _skill = skill;
           // skill.SkillInfo
    }
}