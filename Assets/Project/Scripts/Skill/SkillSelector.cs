using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillSelector : MonoBehaviour
{
    [SerializeField] private List<SkillView> _skillViews;
    [SerializeField] private Button _applyButton;
    
    private List<SkillView> _selectedSkills;
    private SkillView _lastSelectedSkill;
    private int _maxSelectedSkills;
    
    public event Action<List<Skill>> SkillApplyed;

    private void OnEnable()
    {
        _applyButton.onClick.AddListener(OnApplyed);
        
        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked += HandleSkillClicked;
        }
    }

    private void OnDisable()
    {
        _applyButton.onClick.AddListener(OnApplyed);

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked -= HandleSkillClicked;
        }
    }
    
    private void HandleSkillClicked(SkillView skillView)
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
    
    public void ShowSkills(List<Skill> skills, int inputSkillsCount, int outputSkillsCount)
    {
        _maxSelectedSkills = outputSkillsCount;
        inputSkillsCount = Mathf.Min(inputSkillsCount, skills.Count);

        if (inputSkillsCount == 0)
            return;

        for (int i = 0; i < inputSkillsCount; i++)
        {
            int randomIndex = Random.Range(0, skills.Count);
        
            _skillViews[i].gameObject.SetActive(true);
            _skillViews[i].SetSkill(skills[randomIndex]);
        }
        
        HideSkills();
        
        Time.timeScale = 0;
    }

    private void OnApplyed()
    {
        List<Skill> skills = new List<Skill>();

        foreach (var skillView in _skillViews)
            skills.Add(skillView.Skill);
        
        SkillApplyed?.Invoke(skills);
    }
    
    private void HideSkills()
    {
        foreach (SkillView skillView in _skillViews)
        {
            skillView.gameObject.SetActive(false);
        }
    }
}