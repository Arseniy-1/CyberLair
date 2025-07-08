using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Scripts.Services.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Project.Scripts.PlayerSystem.TakeDamageEffect
{
    [Serializable]
    public class LowPassCutoffer
    {
        private const string MasterLowPassCutoff = "MasterLowPassCutoff";
        
        [SerializeField, MinMaxSlider(400, 22000f, showFields: true)] private Vector2 _frequencyCutoff;
        [SerializeField] private float _cutoffDuration = 0.2f;
        [SerializeField] private float _effectDuration = 1f;
        [SerializeField] private AudioMixer _audioMixer;
        
        private Tween _frequencyCutoffTween;
        private CancellationTokenSource _cancellationToken;

        public void StartCutoff()
        {
            CancelCutoff();
            
            Cutoffing(_cancellationToken.Token).Forget();
        }
        
        public void CancelCutoff()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
        }
        
        private void CutoffSound(float endValue)
        {
            _frequencyCutoffTween?.Kill();
            
            _frequencyCutoffTween = _audioMixer
                .DOSetFloat(MasterLowPassCutoff, endValue, _cutoffDuration);
        }
        
        private async UniTaskVoid Cutoffing(CancellationToken token)
        {
            CutoffSound(_frequencyCutoff.x);
            
            await UniTask.Delay(TimeSpan.FromSeconds(_effectDuration), cancellationToken: token);
            
            CutoffSound(_frequencyCutoff.y);
            
            CancelCutoff();
        }
    }
}