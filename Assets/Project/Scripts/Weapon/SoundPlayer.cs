using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 _pitchRange = new Vector2(0.95f, 1.05f);

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        
        _audioSource.playOnAwake = false;
        _audioSource.loop = false;
    }

    private void OnDisable()
    {
        _audioSource.Stop();
    }

    public void Play()
    {
        _audioSource.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        _audioSource.PlayOneShot(_audioSource.clip);
    }
    
    public void PlayAtPoint(Vector2 position)
    {
        GameObject temp = new GameObject("TempAudio");
        temp.transform.position = position;

        AudioSource source = temp.AddComponent<AudioSource>();
        source.clip = _audioSource.clip;
        source.volume = _audioSource.volume;
        source.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        source.spatialBlend = 0f;
        source.Play();

        Destroy(temp, source.clip.length / source.pitch);
    }

    public void PlayLoop()
    {
        _audioSource.loop = true;
        _audioSource.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        _audioSource.Play();
    }
    
    public void StopLoop()
    {
        if (_audioSource.isPlaying && _audioSource.loop)
        {
            _audioSource.Stop();
        }
    }
}