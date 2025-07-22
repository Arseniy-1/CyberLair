using DG.Tweening;
using Project.Scripts.MessageBroker;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class BackgroundMusicFilter : MonoBehaviour
    {
        private readonly CompositeDisposable _disposable = new ();
        
        [SerializeField] private AudioLowPassFilter _lowPassFilter;
        [SerializeField] [MinMaxSlider(0, 22000, true)] private Vector2Int _musicCutoffRange;
        [SerializeField] private float _musicCutoffDuration;

        private Tween _cutoffTween;
        
        private void Awake()
        {
            _lowPassFilter.cutoffFrequency = _musicCutoffRange.y;
        }

        private void OnEnable()
        {
            MessageBrokerHolder.Game
                .Receive<M_GamePaused>()
                .Subscribe(_ => OnGamePaused())
                .AddTo(_disposable);
            
            MessageBrokerHolder.Game
                .Receive<M_GameUnpaused>()
                .Subscribe(_ => OnGameUnpaused())
                .AddTo(_disposable);
        }

        private void OnDisable()
        {
            _disposable.Clear();
            _cutoffTween?.Kill();
        }

        private void OnGamePaused()
        {
            ApplyCutoffFrequency(_musicCutoffRange.x);
        }

        private void OnGameUnpaused()
        {
            ApplyCutoffFrequency(_musicCutoffRange.y);
        }

        private void ApplyCutoffFrequency(float endValue)
        {
            _cutoffTween?.Kill();

            _cutoffTween = DOTween
                .To(() => _lowPassFilter.cutoffFrequency, currentValue => _lowPassFilter.cutoffFrequency = currentValue, endValue, _musicCutoffDuration)
                .SetUpdate(true)
                .SetEase(Ease.InOutSine);
        }
    }
}