using Project.Scripts.ArenaSystem;
using UnityEngine;
using YG;

public class WinScreenHandler : ISubscribable
{
    private readonly Timer _timer;
    private readonly Arena _arena;
    
    private readonly Canvas _winGameCanvas;
    private readonly Canvas _gameCanvas;

    public WinScreenHandler(Timer timer, Arena arena, Canvas winGameCanvas, Canvas gameCanvas)
    {
        _timer = timer;
        _arena = arena;
        
        _winGameCanvas = winGameCanvas;
        _gameCanvas = gameCanvas;
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
            .Publish(new M_GamePaused());

        if (_timer.CurrentSeconds <= YandexGame.savesData.BestTime)
            return;

        YandexGame.savesData.BestTime = _timer.CurrentSeconds;
        YandexGame.SaveProgress();
        YandexGame.NewLBScoreTimeConvert("Leaderboard", YandexGame.savesData.BestTime);
    }
}