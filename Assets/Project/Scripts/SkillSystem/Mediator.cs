using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBroker;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using UniRx;
using UnityEngine;

namespace Project.Scripts.SkillSystem
{
    public class Mediator : MonoBehaviour
    {
        private const int DefaultInputSkillsCount = 3;
        private const int DefaultOutputSkillsCount = 1;
    
        [SerializeField] private List<MutantSkill> _mutantSkills;
        [SerializeField] private List<HardSkill> _hardSkills;
        [SerializeField] private List<Skill> _simpleSkills;
    
        [SerializeField] private Player _player;
        [SerializeField] private GameObject _gameUI;
        [SerializeField] private Level _level;
        [SerializeField] private WeaponHolder _playerWeaponHolder;
        [SerializeField] private Jumper _playerJumper;
        [SerializeField] private SkillSelector _skillSelector;
    
        [SerializeField] private int _startInputSkillsCount;
        [SerializeField] private int _startOutputSkillsCount;

        private SkillHandler _skillHandler;
        private SkillUIHandler _uiHandler;
        private SkillHolder _playerSkillHolder;
        private CancellationTokenSource _cancellationToken;

        private void OnEnable()
        {
            InitializeComponents();
            SubscribeToEvents();
        }

        private void Start()
        {
            _uiHandler.ShowSkillSelection(
                _skillHandler.AvailableSkills,
                _startInputSkillsCount,
                _startOutputSkillsCount);
        }

        private void OnDisable()
        {
            _skillSelector.SkillApplyed -= OnSkillsApplied;
            _level.LevelRaised -= HandleLevelUp;
        
            _cancellationToken.Cancel();
            _playerSkillHolder?.Disable();
        }
    
        private void InitializeComponents()
        {
            _skillHandler = new SkillHandler(_simpleSkills, _mutantSkills, _hardSkills);
            _uiHandler = new SkillUIHandler(_gameUI, _skillSelector);
        
            _cancellationToken = new CancellationTokenSource();
        
            _playerSkillHolder = new SkillHolder(
                new SkillData(_playerWeaponHolder, _player.PlayerStats, _playerJumper),
                _cancellationToken.Token);
        }

        private void SubscribeToEvents()
        {
            _skillSelector.SkillApplyed += OnSkillsApplied;
            _level.LevelRaised += HandleLevelUp;
        
            MessageBrokerHolder.Chest
                .Receive<M_ChestRaised>()
                .Subscribe(_ => HandleLevelUp())
                .AddTo(_cancellationToken.Token);
        }

        private void HandleLevelUp() =>
            _uiHandler.ShowSkillSelection(
                _skillHandler.AvailableSkills, 
                DefaultInputSkillsCount, 
                DefaultOutputSkillsCount);

        private void OnSkillsApplied(List<Skill> skills)
        {
            _skillHandler.ProcessSelectedSkills(skills);
        
            foreach (var skill in skills)
            {
                _playerSkillHolder.CreateSkill(skill);
            }
        
            _uiHandler.CloseSkillSelection();
        }
    }
}