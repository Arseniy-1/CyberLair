using Project.Scripts.ArenaSystem;
using Project.Scripts.MessageBroker;
using UnityEngine;
using YG;

namespace Project.Scripts.CompositionRoot
{
    public class WinScreenHandler : ISubscribable
    {
        private readonly string _leaderboardName;
        
        private readonly Timer _timer;
        private readonly Arena _arena;
    
        private readonly Canvas _winGameCanvas;
        private readonly Canvas _gameCanvas;

        public WinScreenHandler(
            Timer timer, 
            Arena arena, 
            Canvas winGameCanvas, 
            Canvas gameCanvas, 
            string leaderboardName)
        {
            _timer = timer;
            _arena = arena;
        
            _winGameCanvas = winGameCanvas;
            _gameCanvas = gameCanvas;

            _leaderboardName = leaderboardName;
        }
    
        public void Subscribe()
        {
            _arena.WavesDone += ShowWinScreen;
        }

        public void Unsubscribe()
        {
            _arena.WavesDone -= ShowWinScreen;
        }
    
        private void ShowWinScreen()
        {
            _gameCanvas.gameObject.SetActive(false);
            _winGameCanvas.gameObject.SetActive(true);

            MessageBrokerHolder.Game
                .Publish(default(M_GamePaused));

            if (_timer.CurrentSeconds <= YandexGame.savesData.BestTime)
                return;

            YandexGame.savesData.BestTime = _timer.CurrentSeconds;
            YandexGame.SaveProgress();
            YandexGame.NewLBScoreTimeConvert(_leaderboardName, YandexGame.savesData.BestTime);
        }
    }
}