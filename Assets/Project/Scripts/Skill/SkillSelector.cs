using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillSelector : MonoBehaviour
{
    [SerializeField] private List<SkillView> _skillViews;
    [SerializeField] private Button _applyButton;

    [SerializeField] private List<SkillView> _selectedSkills = new List<SkillView>();
    
    [SerializeField] private Sprite _defaultBanner;
    [SerializeField] private Sprite _hardBanner;
    [SerializeField] private Sprite _mutantBanner;
    
    private SkillView _lastSelectedSkill;
    private int _maxSelectedSkills;

    public event Action<List<Skill>> SkillApplyed;

    private void OnEnable()
    {
        _applyButton.onClick.AddListener(OnApplied);

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked += HandleSkillClicked;
        }
    }

    private void OnDisable()
    {
        _applyButton.onClick.RemoveListener(OnApplied);

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked -= HandleSkillClicked;
        }
    }

    private void HandleSkillClicked(SkillView skillView)
    {
        if (_selectedSkills.Contains(skillView))
        {
            skillView.Deselect();
            _selectedSkills.Remove(skillView);
        }
        else
        {
            if (_selectedSkills.Count >= _maxSelectedSkills)
            {
                _lastSelectedSkill.Deselect();
                _selectedSkills.Remove(_lastSelectedSkill);
            }

            skillView.Select();

            _lastSelectedSkill = skillView;
            _selectedSkills.Add(skillView);
        }

        if (_selectedSkills.Count >= _maxSelectedSkills)
            _applyButton.gameObject.SetActive(true);
        else
            _applyButton.gameObject.SetActive(false);
    }

    public void ShowSkills(List<Skill> skills, int inputSkillsCount, int outputSkillsCount)
    {
        gameObject.SetActive(true);
        _maxSelectedSkills = outputSkillsCount;
    
        inputSkillsCount = Mathf.Min(inputSkillsCount, skills.Count);
        
        if (inputSkillsCount == 0)
            return;
    
        Time.timeScale = 0;
        
        List<Skill> shuffledSkills = skills.OrderBy( skill => Random.value ).ToList( );
    
        for (int i = 0; i < inputSkillsCount; i++)
        {
            var selectedSkill = shuffledSkills[i];
            _skillViews[i].gameObject.SetActive(true);

            switch (selectedSkill)
            {
                case MutantSkill:
                    _skillViews[i].SetSkill(selectedSkill, _mutantBanner);
                    break;
                
                case HardSkill:
                    _skillViews[i].SetSkill(selectedSkill, _hardBanner);
                    break;
                
                default:
                    _skillViews[i].SetSkill(selectedSkill, _defaultBanner);
                    break;
            }
        }
    }

    private void OnApplied()
    {
        HideSkills();
        
        List<Skill> skills = _selectedSkills.Select(skillView => skillView.Skill).ToList();

        _lastSelectedSkill = null;
        _selectedSkills = new List<SkillView>();
        _applyButton.gameObject.SetActive(false);
        gameObject.SetActive(false);
        
        SkillApplyed?.Invoke(skills);
    }

    private void HideSkills()
    {
        foreach (SkillView skillView in _skillViews)
        {
            skillView.Deselect();
            skillView.gameObject.SetActive(false);
        }
    }
}