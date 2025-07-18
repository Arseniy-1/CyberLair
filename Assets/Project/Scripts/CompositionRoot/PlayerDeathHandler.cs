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

        private readonly string _leaderboardName;
        private readonly Player _player;
        private readonly Timer _timer;
    
        private readonly EndGameCanvas _endGameCanvas;
        private readonly EndGameCanvas _continueScreenCanvas;
        private readonly Canvas _gameCanvas;
        private readonly int _triesCount;
    
        private int _currentTriesCount;
    
        public PlayerDeathHandler(
            Player player,
            Timer timer,
            EndGameCanvas endGameCanvas,
            EndGameCanvas continueScreenCanvas,
            Canvas gameCanvas,
            int triesCount,
            string leaderboardName)
        {
            _player = player;
            _timer = timer;
        
            _endGameCanvas = endGameCanvas;
            _continueScreenCanvas = continueScreenCanvas;
            _gameCanvas = gameCanvas;
        
            _triesCount = triesCount;
            _leaderboardName = leaderboardName;
        }
    
        public void Subscribe()
        {
            _player.OnDeath += OnPlayerDied;
            YG2.onRewardAdv += OnRewarded;
        }

        public void Unsubscribe()
        {
            _player.OnDeath -= OnPlayerDied;
            YG2.onRewardAdv -= OnRewarded;
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
        
            if (_timer.CurrentSeconds <= YG2.saves.BestTime)
                return;
            
            YG2.saves.BestTime = _timer.CurrentSeconds;
            
            YG2.SaveProgress();
            YG2.SetLBTimeConvert(_leaderboardName, YG2.saves.BestTime);
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
    
        private void OnRewarded(string id)
        {
            if (id == nameof(RewardedAdType.SecondChance))
                BringBackPlayer();
        }
    }
}