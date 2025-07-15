using UnityEngine.Audio;
using DG.Tweening;

namespace Project.Scripts.Services.Extensions
{
    public static class AudioMixerExtensions
    {
        public static Tweener DoSetFloat(this AudioMixer mixer, string name, float endValue, float duration)
        {
            mixer.GetFloat(name, out var startValue);
            
            return DOTween.To(
                () => startValue,
                value => mixer.SetFloat(name, value),
                endValue,
                duration
            );
        }
    }
}