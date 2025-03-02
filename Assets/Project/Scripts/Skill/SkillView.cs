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
    
    public void SetSkill(Skill skill)
    {
        _skill = skill;
        _nameText.text = _skill.SkillInfo.SkillName;
        _descriptionText.text = _skill.SkillInfo.Description;
        _image.sprite = _skill.SkillInfo.Icon;
    }

    public void Select()
    {
        gameObject.SetActive(false);
    }

    public void Deselect()
    {
        gameObject.SetActive(true);
    }
    
    private void HandleClick()
    {
        OnClicked?.Invoke(this);
    }
}