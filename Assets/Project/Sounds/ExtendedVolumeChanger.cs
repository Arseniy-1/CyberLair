using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ExtendedVolumeChanger : MonoBehaviour
{
    private const float MinVolume = -80;
    
    [SerializeField] private Toggle _button;
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _slider;

    [SerializeField] private bool _isEnabled = true;

    private void OnDestroy()
    {
        _button.onValueChanged.RemoveListener(ToggleMusic);
        _slider.onValueChanged.RemoveListener(Change);
    }

    public void Initialize()
    {
        _button.onValueChanged.AddListener(ToggleMusic);
        _slider.onValueChanged.AddListener(Change);
    }
    
    private void ToggleMusic(bool isMuted)
    {
        if (isMuted)
            _audioMixer.audioMixer.SetFloat(_audioMixer.name, MinVolume);
        else
            SetCurrentVolume(_slider.value);

        _isEnabled = !isMuted;
    }

    private void Change(float amount)
    {
        if (_isEnabled)
            SetCurrentVolume(amount);
    }

    private void SetCurrentVolume(float volume)
    {
        _audioMixer.audioMixer.SetFloat(_audioMixer.name, Mathf.Log10(volume) * 40);
    }
}