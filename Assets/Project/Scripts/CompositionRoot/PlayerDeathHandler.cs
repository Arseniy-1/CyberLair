using Project.Scripts.MessageBroker;
using Project.Scripts.PlayerSystem;
using Project.Scripts.UI;
using UnityEngine;
using YG;

namespace Project.Scripts.CompositionRoot
{
    public class PlayerDeathHandler : ISubscribable
    {
        private const float InvulnerabilityTime = 2.5f;
        private const string LeaderboardName = "time";
    
        private readonly Player _player;
        private readonly Timer _timer;
    
        private readonly EndGameCanvas _endGameCanvas;
        private readonly EndGameCanvas _continueScreenCanvas;
        private readonly Canvas _gameCanvas;
        private readonly int _triesCount;
    
        private int _currentTriesCount;
    
        public PlayerDeathHandler(Player player, Timer timer, EndGameCanvas endGameCanvas, EndGameCanvas continueScreenCanvas,
            Canvas gameCanvas, int triesCount)
        {
            _player = player;
            _timer = timer;
        
            _endGameCanvas = endGameCanvas;
            _continueScreenCanvas = continueScreenCanvas;
            _gameCanvas = gameCanvas;
        
            _triesCount = triesCount;
        }
    
        public void Subscribe()
        {
            _player.OnDeath += OnPlayerDied;
            YandexGame.RewardVideoEvent += OnRewarded;
        }

        public void Unsubscribe()
        {
            _player.OnDeath -= OnPlayerDied;
            YandexGame.RewardVideoEvent -= OnRewarded;
        }
    
        private void OnPlayerDied()
        {
            _gameCanvas.gameObject.SetActive(false);
        
            MessageBrokerHolder.Game
                .Publish(default(M_GamePaused));
        
            if (_currentTriesCount < _triesCount)
                ShowContinueScreen();
            else
                ShowEndGameScreen();
        }

        private void ShowContinueScreen()
        {
            _continueScreenCanvas.gameObject.SetActive(true);
            _continueScreenCanvas.ShowStats(_timer.CurrentTime);
        
            if (_timer.CurrentSeconds <= YandexGame.savesData.BestTime)
                return;

            YandexGame.savesData.BestTime = _timer.CurrentSeconds;
        
            YandexGame.SaveProgress();
            YandexGame.NewLBScoreTimeConvert(LeaderboardName, YandexGame.savesData.BestTime);
        }
    
        private void ShowEndGameScreen()
        {
            _endGameCanvas.gameObject.SetActive(true);
            _endGameCanvas.ShowStats(_timer.CurrentTime);
        }
    
        private void BringBackPlayer()
        {
            _currentTriesCount++;
        
            _player.Revive(InvulnerabilityTime);

            MessageBrokerHolder.Game
                .Publish(default(M_GameUnpaused));

            _gameCanvas.gameObject.SetActive(true);
            _continueScreenCanvas.gameObject.SetActive(false);
        }
    
        private void OnRewarded(int id)
        {
            if (id == (int)RewardedAdType.SecondChance)
                BringBackPlayer();
        }
    }
}