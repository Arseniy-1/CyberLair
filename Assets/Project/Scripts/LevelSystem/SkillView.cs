using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;
    
    private ISkill _skill;
    
    public event Action<ISkill> OnClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }
    
    private void HandleClick()
    {
        OnClicked?.Invoke(_skill);
    }
    
    public void SetSkill(ISkill skill)
    {
        _skill = skill;
        _nameText.text = _skill.SkillInfo.SkillName.ToString();
    }
}