using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeChanger : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _audioMixer;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(Change);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(Change);
    }

    private void Change(float value)
    {
        _audioMixer.audioMixer.SetFloat(_audioMixer.name, Mathf.Log10(_slider.value) * 40);
    }
}