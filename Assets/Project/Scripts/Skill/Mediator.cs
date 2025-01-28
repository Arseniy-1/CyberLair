using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

public class Mediator : MonoBehaviour
{
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private List<SkillView> _skillViews;

    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private WeaponHolder _playerWeaponHolder;

    private readonly SkillHolder _skillHolder = new();
    private int _skillsCount = 3;

    private PlayerStats _playerStats;
    private PlayerStats _startPlayerStats;

    private void OnEnable()
    {
        _playerStats = _player.PlayerStats;
        _startPlayerStats = _player.PlayerStats.DeepCopy();
        
        _level.LevelRaised += ShowSkills;

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked += OnSkillApplied;
        }

        ShowSkills();
    }

    private void OnDisable()
    {
        _level.LevelRaised -= ShowSkills;

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked -= OnSkillApplied;
        }
    }

    [Button]
    private void OnSkillApplied(Skill skill)
    {
        _skillHolder.AddSkill(skill);

        var skillData = new SkillData(_playerWeaponHolder, _playerStats, _startPlayerStats,
            _skillHolder.Skills[skill]);

        skill.Apply(skillData);

        HideSkills();
        
        Time.timeScale = 1;
    }

    private void ShowSkills()
    {
        Time.timeScale = 0;
        
        if (_skillViews.IsNullOrEmpty())
            return;
        
        List<Skill> availableSkills = _skills
            .Where(skill => !_skillHolder.Skills.TryGetValue(skill, out int skillLevel) || skillLevel < skill.MaxLevel - 1)
            .ToList();
        
        for (int i = 0; i < _skillsCount; i++)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);

            _skillViews[i].gameObject.SetActive(true);
            _skillViews[i].SetSkill(availableSkills[randomIndex]);

            availableSkills.RemoveAt(randomIndex);
        }
    }

    private void HideSkills()
    {
        foreach (SkillView skillView in _skillViews)
        {
            skillView.gameObject.SetActive(false);
        }
    }
}