using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Assets.SimpleLocalization.Scripts;

public class SkillView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _skillIcon;
    [SerializeField] private Image _skillBanner;
    [SerializeField] private Material _selectedMaterial;
    
    private Skill _skill;
    
    public Skill Skill => _skill;
    
    public event Action<SkillView> OnClicked;

    private void OnEnable()
    {
        _button.onClick.AddListener(HandleClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(HandleClick);
    }
    
    public void SetSkill(Skill skill, Sprite skillBanner)
    {
        _skill = skill;
        _nameText.text = LocalizationManager.Localize(_skill.SkillInfo.SkillName);
        _descriptionText.text = LocalizationManager.Localize(_skill.SkillInfo.Description);
        _skillBanner.sprite = skillBanner;
        _skillIcon.sprite = _skill.SkillInfo.Icon;
    }

    public void Select()
    {
        _skillBanner.material = _selectedMaterial;
    }

    public void Deselect()
    {
        _skillBanner.material = null;
    }
    
    private void HandleClick()
    {
        OnClicked?.Invoke(this);
    }
}