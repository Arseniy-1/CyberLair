using System;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Skill
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Image _skillIcon;
        [SerializeField] private Image _skillBanner;
        [SerializeField] private Material _selectedMaterial;

        public global::Project.Scripts.Skill.Skill Skill { get; private set; }

        public event Action<SkillView> OnClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleClick);
        }
    
        public void SetSkill(global::Project.Scripts.Skill.Skill skill, Sprite skillBanner)
        {
            Skill = skill;
            _nameText.text = LocalizationManager.Localize(Skill.SkillInfo.SkillName);
            _descriptionText.text = LocalizationManager.Localize(Skill.SkillInfo.Description);
            _skillBanner.sprite = skillBanner;
            _skillIcon.sprite = Skill.SkillInfo.Icon;
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
}