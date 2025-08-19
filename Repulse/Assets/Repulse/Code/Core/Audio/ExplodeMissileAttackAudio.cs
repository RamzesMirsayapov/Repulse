using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeMissileAttackAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _decoyExplosionSound;

    [SerializeField] private float _volume;

    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    [SerializeField] private ExplosionAttack _explosionAttack;
    [SerializeField] private DecoyExplosionAttack _decoyExplosionAttack;

    private void OnEnable()
    {
        _explosionAttack.OnExploded += PlayExplosionAttackSound;

        _decoyExplosionAttack.OnExploded += PlayExplosionAttackSound;
        _decoyExplosionAttack.OnDecoyExploded += PlayDecoyExplosionAttackSound;
    }

    private void OnDisable()
    {
        _explosionAttack.OnExploded -= PlayExplosionAttackSound;

        _decoyExplosionAttack.OnExploded -= PlayExplosionAttackSound;
        _decoyExplosionAttack.OnDecoyExploded -= PlayDecoyExplosionAttackSound;
    }

    private void PlayExplosionAttackSound()
    {
        _audioSource.volume = _volume;
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(_explosionSound);
    }

    private void PlayDecoyExplosionAttackSound()
    {
        _audioSource.volume = _volume;
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(_decoyExplosionSound);
    }
}
