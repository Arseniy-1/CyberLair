using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SkillSelector : MonoBehaviour
{
    [SerializeField] private List<SkillView> _skillViews;
    [SerializeField] private Button _applyButton;

    [SerializeField] private List<SkillView> _selectedSkills = new List<SkillView>();
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
        _applyButton.onClick.RemoveListener(OnApplyed);

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
        Time.timeScale = 0;

        gameObject.SetActive(true);

        List<Skill> availableSkills = skills;

        _maxSelectedSkills = outputSkillsCount;
        inputSkillsCount = Mathf.Min(inputSkillsCount, skills.Count);

        if (inputSkillsCount == 0)
            return;

        for (int i = 0; i < inputSkillsCount; i++)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);

            _skillViews[i].gameObject.SetActive(true);
            _skillViews[i].SetSkill(availableSkills[randomIndex]);

            availableSkills.RemoveAt(randomIndex);
        }
    }

    private void OnApplyed()
    {
        HideSkills();
        
        gameObject.SetActive(false);

        List<Skill> skills = new List<Skill>();

        foreach (var skillView in _selectedSkills)
            skills.Add(skillView.Skill);

        SkillApplyed?.Invoke(skills);
        Time.timeScale = 1;
    }

    private void HideSkills()
    {
        foreach (SkillView skillView in _selectedSkills)
        {
            skillView.Deselect();
            skillView.gameObject.SetActive(false);
        }
    }
}