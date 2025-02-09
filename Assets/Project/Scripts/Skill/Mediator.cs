using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine.Serialization;

public class Mediator : MonoBehaviour
{
    [SerializeField] private List<HardSkill> _hardSkills;
    [SerializeField] private List<Skill> _availableSkills;
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

        var skillData = new SkillData(_playerWeaponHolder, _playerStats, _startPlayerStats, _skillHolder.Skills[skill]);

        skill.Apply(skillData);

        HideSkills();
        
        Time.timeScale = 1;
    }

    private void ShowSkills()
    {
        if (_skillViews.IsNullOrEmpty())
            return;
        
        List<Skill> availableSkills = _availableSkills
            .Where(skill => !_skillHolder.Skills.TryGetValue(skill, out int skillLevel))
            .ToList();
        
        if (availableSkills.Count == 0)
            return;
        
        int cappedSkillsCount = availableSkills.Count >= _skillsCount ? _skillsCount : availableSkills.Count;

        for (int i = 0; i < cappedSkillsCount; i++)
        {
            int randomIndex = Random.Range(0, availableSkills.Count);

            _skillViews[i].gameObject.SetActive(true);
            _skillViews[i].SetSkill(availableSkills[randomIndex]);

            availableSkills.RemoveAt(randomIndex);
        }
        
        Time.timeScale = 0;
    }

    private void HideSkills()
    {
        foreach (SkillView skillView in _skillViews)
        {
            skillView.gameObject.SetActive(false);
        }
    }
}