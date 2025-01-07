using UnityEngine;
using System.Collections.Generic;

public class Mediator : MonoBehaviour
{
    [SerializeField] private List<SkillView> _skillViews;
    [SerializeField] private List<Skill> _skills;
    
    [SerializeField] private Player _player;
    [SerializeField] private PlayerConfig _playerConfig;
    [SerializeField]private WeaponHolder _playerWeaponHolder;
    
    private SkillHolder _skillHolder = new SkillHolder();

    private void OnEnable()
    {
        foreach (var skillView in _skillViews)
        {
            skillView.OnClicked += OnSkillApplyed;
            skillView.SetSkill(_skills[0]);
        }
    }

    private void OnSkillApplyed(Skill skill)
    {
        _skillHolder.AddSkill(skill);

        if (skill is PassiveSkill passiveSkill)
        {
            passiveSkill.Apply(_player.PlayerStats, _playerConfig, _skillHolder.Skills[skill]);
        }
        else if (skill is ActiveSkill activeSkill)
        {
            activeSkill.Apply(_playerWeaponHolder);
        }
    }

    private void ShowSkills(int skillCount)
    {
        //skillViews - отображаем на сцене

        for (int i = 0; i < skillCount; i++)
        {
            _skillViews[i].SetSkill(_skills[Random.Range(0, _skills.Count)]);
        }
    }
}