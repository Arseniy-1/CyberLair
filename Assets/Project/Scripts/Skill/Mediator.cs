using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniRx;

public class Mediator : MonoBehaviour
{
    [SerializeField] private List<MutantSkill> _mutantSkills;
    [SerializeField] private List<HardSkill> _hardSkills;
    [SerializeField] private List<Skill> _simpleSkills;

    [SerializeField] private List<Skill> _availableSkills;
    [SerializeField] private List<Skill> _raisedSkills;

    [SerializeField] private Player _player;
    [SerializeField] private GameObject _gameUI;
    [SerializeField] private Level _level;
    [SerializeField] private WeaponHolder _playerWeaponHolder;
    [SerializeField] private Jumper _playerJumper;

    [SerializeField] private SkillSelector _skillSelector;
    [SerializeField] private int _startInputSkillsCount;
    [SerializeField] private int _startOutputSkillsCount;

    private SkillHolder _playerSkillHolder;
    private PlayerStats _playerStats;
    
    private readonly CompositeDisposable _disposable = new();

    private void OnEnable()
    {
        _skillSelector.SkillApplyed += OnSkillsApplied;
        _level.LevelRaised += HandleLevelUp;

        _availableSkills.AddRange(_simpleSkills);
        _simpleSkills = null;

        _playerStats = _player.PlayerStats;
        _playerSkillHolder = new SkillHolder(new SkillData(_playerWeaponHolder, _playerStats, _playerJumper));

        
        MessageBrokerHolder.Chest.Receive<M_ChestRaised>().Subscribe((message) => HandleRaiseChest())
            .AddTo(_disposable);
    }

    private void OnDisable()
    {
        _skillSelector.SkillApplyed -= OnSkillsApplied;
        _level.LevelRaised -= HandleLevelUp;
    }
    
    private void Start()
    {
        ShowSkills(_availableSkills, _startInputSkillsCount, _startOutputSkillsCount);
    }

    private void HandleRaiseChest()
    {
        HandleLevelUp();
    }
    
    private void ShowSkills(List<Skill> skills, int inputSkillsCount, int outputSkillsCount)
    {
        _player.PlayerInputController.PlayerInput.Disable();
        _gameUI.gameObject.SetActive(false);
        _skillSelector.ShowSkills(skills, inputSkillsCount, outputSkillsCount);
    }

    private void HandleLevelUp()
    {
        int inputSkillsCount = 3;
        int outputSkillsCount = 1;

        ShowSkills(_availableSkills, inputSkillsCount, outputSkillsCount);
    }

    [Button]
    private void OnSkillsApplied(List<Skill> skills)
    {
        foreach (var skill in skills)
        {
            _availableSkills.Remove(skill);
            _raisedSkills.Add(skill);

            if(_hardSkills.Contains(skill))
                _hardSkills.Remove((HardSkill)skill);
            
            if(_mutantSkills.Contains(skill))
                _mutantSkills.Remove((MutantSkill)skill);

            foreach (HardSkill hardSkill in _hardSkills.Where(hardSkill => hardSkill.IsAvailable(_raisedSkills)))
            { 
                if(_availableSkills.Contains(hardSkill) == false)
                    _availableSkills.Add(hardSkill);
            }

            foreach (MutantSkill mutantSkill in _mutantSkills.Where(mutantSkill =>
                         mutantSkill.IsAvailable(_raisedSkills)))
            {
                if(_availableSkills.Contains(mutantSkill) == false)
                    _availableSkills.Add(mutantSkill);
            }
            
            _playerSkillHolder.CreateSkill(skill);
        }

        Time.timeScale = 1;
        _player.PlayerInputController.PlayerInput.Enable();
        _gameUI.gameObject.SetActive(true);
    }
}