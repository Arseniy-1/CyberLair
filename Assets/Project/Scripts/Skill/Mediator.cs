using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

public class Mediator : MonoBehaviour
{
    [SerializeField] private List<HardSkill> _hardSkills;
    [SerializeField] private List<MutantSkill> _mutantSkills;
    [SerializeField] private List<Skill> _simpleSkills;

    [SerializeField] private List<Skill> _availableSkills;
    [SerializeField] private List<Skill> _raisedSkills;
    [SerializeField] private List<SkillView> _skillViews;

    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private WeaponHolder _playerWeaponHolder;

    private readonly int _skillsCount = 3;

    private PlayerStats _playerStats;

    private void OnEnable()
    {
        _availableSkills.AddRange(_simpleSkills);

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
        _availableSkills.Remove(skill);
        _raisedSkills.Add(skill);

        foreach (var hardSkill in _hardSkills.Where(hardSkill => hardSkill.IsAvailable(_raisedSkills)))
        {
            _availableSkills.Add(hardSkill);
        }
        
        foreach (var mutantSkill in _mutantSkills.Where(mutantSkill => mutantSkill.IsAvailable(_raisedSkills)))
        {
            _availableSkills.Add(mutantSkill);
        }
        
        var skillData = new SkillData(_playerWeaponHolder, _playerStats);

        skill.Apply(skillData);

        HideSkills();

        Time.timeScale = 1;
    }

    [Button]
    private void ShowSkills()
    {
        for (int i = 0; i < _skillsCount; i++)
        {
            int randomIndex = Random.Range(0, _availableSkills.Count);
            
            _skillViews[i].gameObject.SetActive(true);
            _skillViews[i].SetSkill(_availableSkills[randomIndex]);
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