using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

public class Mediator : MonoBehaviour
{
    [SerializeField] private List<Skill> _skills;
    [SerializeField] private List<SkillView> _skillViews;

    [SerializeField] private Player _player;
    [SerializeField] private Level _level;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField] private WeaponHolder _playerWeaponHolder;

    private SkillHolder _skillHolder = new();
    private int _skillsCount = 3;

    private void OnEnable()
    {
        _level.LevelRaised += ShowSkills;

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked += OnSkillApplyed;
        }

        ShowSkills();
    }

    private void OnDisable()
    {
        _level.LevelRaised -= ShowSkills;

        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked -= OnSkillApplyed;
        }
    }

    [Button]
    private void OnSkillApplyed(ISkill skill)
    {
        Debug.Log($"{skill.GetType()}");
        
        _skillHolder.AddSkill(skill);

        switch (skill)
        {
            case PassiveSkill passiveSkill:
                passiveSkill.Apply(_player.PlayerStats, _playerConfig, _skillHolder.Skills[skill]);
                break;
            case ActiveSkill activeSkill:
                Debug.Log($"{_skillHolder.Skills[skill]}");
                activeSkill.Apply(_playerWeaponHolder, _skillHolder.Skills[skill]);
                break;
        }

        HideSkills();
    }

    private void ShowSkills()
    {
        var availableSkills = _skills.ToList();

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
        for (int i = 0; i < _skillViews.Count; i++)
        {
            _skillViews[i].gameObject.SetActive(false);
        }
    }
}