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
        
        [SerializeField] [MinMaxSlider(400, 22000f, showFields: true)] private Vector2 _frequencyCutoff;
        [SerializeField] private float _cutoffDuration = 0.2f;
        
        [SerializeField] [MinMaxSlider(1, 4, showFields: true)] 
        private Vector2 _cutoffDurationRange = new (1, 4);
        
        [SerializeField] [MinMaxSlider(20f, 70f, showFields: true)] 
        private Vector2 _damageThresholdRange = new (20, 70);
        
        [SerializeField] private AudioMixer _audioMixer;
        
        private Tween _frequencyCutoffTween;
        private CancellationTokenSource _cancellationToken;

        public void StartCutoff(float damageAmount)
        {
            CancelCutoff();
            
            float damageThresholdRangeDelta = _damageThresholdRange.y - _damageThresholdRange.x;
            float damageThreshold = damageAmount - _damageThresholdRange.x;
            float normalizedDamage = Mathf.Clamp01(damageThreshold / damageThresholdRangeDelta);
            
            float calculatedDuration = Mathf.Lerp(_cutoffDurationRange.x, _cutoffDurationRange.y, normalizedDamage);
            
            Cutoffing(calculatedDuration, _cancellationToken.Token).Forget();
        }
        
        public void CancelCutoff()
        {
            _cancellationToken?.Cancel();
            _cancellationToken = new CancellationTokenSource();
            
            _audioMixer.SetFloat(MasterLowPassCutoff, _frequencyCutoff.y);
        }
        
        private void CutoffSound(float endValue)
        {
            _frequencyCutoffTween?.Kill();
            
            _frequencyCutoffTween = _audioMixer
                .DoSetFloat(MasterLowPassCutoff, endValue, _cutoffDuration);
        }
        
        private async UniTaskVoid Cutoffing(float duration, CancellationToken token)
        {
            CutoffSound(_frequencyCutoff.x);
            
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: token);
            
            CutoffSound(_frequencyCutoff.y);
            
            CancelCutoff();
        }
    }
}