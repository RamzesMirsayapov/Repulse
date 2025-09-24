using UnityEngine;

public class BaseExplosionAudio : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected AudioSource _audioSource;

    [SerializeField] protected float _volume;

    [SerializeField] protected float _minPitch;
    [SerializeField] protected float _maxPitch;

    [SerializeField] protected bool _useRandomPitch;

    protected void PlaySound(AudioClip audioClip)
    {
        _audioSource.volume = _volume;

        if(_useRandomPitch)
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        else
            _audioSource.pitch = 1f;

        _audioSource.PlayOneShot(audioClip);
    }
}