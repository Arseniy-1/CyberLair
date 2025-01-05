using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSkillConfig : MonoBehaviour
{
    private Player _player;
    private List<Skill> _skills;

    private void Update()
    {
        foreach (var skill in _skills)
        {
            skill.Update(); 
        }
    }
}

public class Skill
{
    [SerializeField] private SkillView _view;
    
    private int _ricochetCount;
    private int _damage;
    
    public int Level { get; private set; }
    
    public void Initialize()
    {
        _view.OnClicked += Updgrade;
    }

    public void Update()
    {
        
    }

    private void Updgrade()
    {
        Level++;
        OnUpgraded();
    }

    private void OnUpgraded()
    {
        int _damagePerUpgrade = _damage / 2;
        
        _ricochetCount++;
        _damage += _damagePerUpgrade;
    }
}

public class SkillView
{
    public event Action OnClicked;
}

public class Level : MonoBehaviour
{
    private ExperienceStorage _experienceStorage;
    private int _currentLevel;

    public event Action LevelRaised;
    
    private void OnEnable()
    {
        _experienceStorage.LevelRaised += UpgradeLevel;
    }

    private void UpgradeLevel()
    {
        _currentLevel++;
        LevelRaised?.Invoke();
    }
}
