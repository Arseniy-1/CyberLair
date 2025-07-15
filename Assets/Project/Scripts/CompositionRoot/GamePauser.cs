using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.MessageBroker;
using UniRx;
using UnityEngine;

namespace Project.Scripts.CompositionRoot
{
    public class GamePauser
    {
        public GamePauser(CancellationToken token)
        {
            MessageBrokerHolder.Game
                .Receive<M_GamePaused>()
                .Subscribe(_ => PauseGame())
                .AddTo(token);
            
            MessageBrokerHolder.Game
                .Receive<M_GameUnpaused>()
                .Subscribe(_ => UnpauseGame())
                .AddTo(token);
        }
        
        private void PauseGame()
        {
            Time.timeScale = 0;
        }

        private void UnpauseGame()
        {
            Time.timeScale = 1;
        }
    }
}