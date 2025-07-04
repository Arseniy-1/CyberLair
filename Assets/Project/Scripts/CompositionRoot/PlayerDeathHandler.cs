using UnityEngine;
using YG;

public class PlayerDeathHandler : ISubscribable
{
    private readonly Player _player;
    private readonly Timer _timer;
    
    private readonly EndGameCanvas _endGameCanvas;
    private readonly Canvas _gameCanvas;
    
    public PlayerDeathHandler(Player player, Timer timer, EndGameCanvas endGameCanvas, Canvas gameCanvas)
    {
        _player = player;
        _timer = timer;
        
        _endGameCanvas = endGameCanvas;
        _gameCanvas = gameCanvas;
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
        _endGameCanvas.gameObject.SetActive(true);
        _endGameCanvas.ShowStats(_timer.CurrentTime);
            
        MessageBrokerHolder.Game
            .Publish(new M_GamePaused());
            
        if (_timer.CurrentSeconds <= YandexGame.savesData.BestTime)
            return;

        YandexGame.savesData.BestTime = _timer.CurrentSeconds;
        
        YandexGame.SaveProgress();
        YandexGame.NewLBScoreTimeConvert("Leaderboard", YandexGame.savesData.BestTime);
    }
    
    private void BringBackPlayer()
    {
        _player.PlayerStats.Health.Heal(_player.PlayerStats.Health.MaxHealth);

        float invulnerabilityTime = 2.5f;
        _player.TakeImmortality(invulnerabilityTime);

        MessageBrokerHolder.Game
            .Publish(new M_GameUnpaused());

        _gameCanvas.gameObject.SetActive(true);
        _endGameCanvas.gameObject.SetActive(false);
    }
    
    private void OnRewarded(int id)
    {
        if (id == (int)RewardedAdType.SecondChance)
            BringBackPlayer();
    }
}