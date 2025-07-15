using System.Collections.Generic;
using System.Threading;
using Assets.SimpleLocalization.Scripts;
using Project.Scripts.ArenaSystem;
using Project.Scripts.MapGenerationSystem;
using Project.Scripts.MessageBroker;
using Project.Scripts.PlayerSystem;
using Project.Scripts.Services;
using Project.Scripts.Skill;
using Project.Scripts.Spawners.Enemies;
using Project.Scripts.Stats.View;
using Project.Scripts.UI;
using UnityEngine;
using YG;

namespace Project.Scripts.CompositionRoot
{
    public class CompositionRoot : MonoBehaviour
    {
        [SerializeField] private Arena _arena;
        [SerializeField] private MapGenerator _mapGenerator;
        [SerializeField] private BossHandler _bossHandler;
        [SerializeField] private Player _player;
        [SerializeField] private MainEnemySpawner _mainEnemySpawner;
        [SerializeField] private EdgeSpawner _edgeSpawner;
        [SerializeField] private List<EnemyDespawner> _enemyDespawners;
        [SerializeField] private Level _level;
        [SerializeField] private Timer _timer;

        [SerializeField] private StatsBar _HealthBar;
        [SerializeField] private StatsText _HealthText;

        [SerializeField] private StatsBar _experienceBar;

        [SerializeField] private StatsBar _shieldBar;

        [SerializeField] private int _triesCount;
        [SerializeField] private EndGameCanvas _endGameCanvas;
        [SerializeField] private EndGameCanvas _continueScreenCanvas;
        [SerializeField] private Canvas _winGameCanvas;
        [SerializeField] private Canvas _gameCanvas;
        [SerializeField] private TutorialWindow _tutorialView;

        private List<ISubscribable> _subscribables;
        
        private Coroutine _invulnerability;
        private CancellationTokenSource _cancellationToken;
        
        private GamePauser _gamePauser;

        private void Awake()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            _gamePauser = new GamePauser(_cancellationToken.Token);

            var playerDeathHandler = new PlayerDeathHandler(
                _player, 
                _timer, 
                _endGameCanvas, 
                _continueScreenCanvas, 
                _gameCanvas, 
                _triesCount
                );
            
            var winScreenHandler = new WinScreenHandler(
                _timer, 
                _arena, 
                _winGameCanvas, 
                _gameCanvas
                );
            
            _subscribables = new List<ISubscribable>
            {
                playerDeathHandler,
                winScreenHandler,
            };
            
            _edgeSpawner.SpawnOnEdges();
            _mapGenerator.Initialize();
            _bossHandler.Initialize(_player.transform, _cancellationToken.Token);

            _mainEnemySpawner.Initialize(_player, _edgeSpawner.EdgeObjects, _enemyDespawners);

            _arena.Initialize(_mainEnemySpawner, _cancellationToken.Token);
            _arena.Work();

            _level.Initialize(_player.ExperienceStorage);

            _shieldBar.Initialize(_player.PlayerStats.ShieldAmount);

            _HealthBar.Initialize(_player.PlayerStats.Health);
            _HealthText.Initialize(_player.PlayerStats.Health);

            _experienceBar.Initialize(_player.ExperienceStorage);

            LocalizationManager.Read();
            LocalizationManager.Language = YandexGame.lang;
        }

        private void OnEnable()
        {
            _subscribables.ForEach(subscribable => subscribable.Subscribe());
            
            if (YandexGame.savesData.isFirstSession == false) 
                return;
            
            _tutorialView.gameObject.SetActive(true);
            _tutorialView.OnFinished += OnTutorialFinished;
            
            MessageBrokerHolder.Game
                .Publish(new M_GamePaused());

            YandexGame.savesData.isFirstSession = false;
            YandexGame.SaveProgress();
        }

        private void OnDisable()
        {
            _cancellationToken?.Cancel();
            
            _subscribables.ForEach(subscribable => subscribable.Unsubscribe());
        }

        private void OnTutorialFinished()
        {
            _tutorialView.gameObject.SetActive(false);
            
            _tutorialView.OnFinished -= OnTutorialFinished;
        }
    }
}