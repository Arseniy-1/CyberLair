using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Project.Scripts.Settings
{
    public class VolumeChanger : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _audioMixer;
        [SerializeField] private Slider _slider;

        private void OnDestroy()
        {
            _slider.onValueChanged.RemoveListener(Change);
        }

        public void Initialize()
        {
            _slider.onValueChanged.AddListener(Change);
        }
    
        private void Change(float value)
        {
            _audioMixer.audioMixer.SetFloat(_audioMixer.name, Mathf.Log10(_slider.value) * 40);
        }
    }
}