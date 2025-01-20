using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _image;
    
    private Skill _skill;
    
    public event Action<Skill> OnClicked;

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
    
    public void SetSkill(Skill skill)
    {
        _skill = skill;
        _nameText.text = _skill.SkillInfo.SkillName;
        _descriptionText.text = _skill.SkillInfo.Description;
        _image.sprite = _skill.SkillInfo.Icon;
    }
}