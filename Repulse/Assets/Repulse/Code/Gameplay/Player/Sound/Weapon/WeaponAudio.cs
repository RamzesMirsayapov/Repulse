using UnityEngine;

public class WeaponAudio : MonoBehaviour
{
    [SerializeField] private WeaponAttack _weaponAttack;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _shotSound;

    [Header("Volume")]
    [SerializeField] private float _volume;

    [Header("AudioPitch")]
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    private void OnEnable()
    {
        _weaponAttack.OnAttack += PlayShotSound;
    }

    private void OnDisable()
    {
        _weaponAttack.OnAttack -= PlayShotSound;
    }

    private void PlayShotSound()
    {
        _audioSource.volume = _volume;
        //_audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(_shotSound);
    }
}
