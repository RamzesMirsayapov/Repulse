using System.Collections.Generic;
using UnityEngine;

public class FootStepSound : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    [Header("Volume")]
    [SerializeField] private float _volume;

    [Header("AudioPitch")]
    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    [SerializeField] private float _stepRate;

    private float _stepCoolDown;

    private void Update()
    {
        _stepCoolDown -= Time.deltaTime;

        if ((Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) && _stepCoolDown <= 0f)
        {
            PlayFootStepSound();

            _stepCoolDown = _stepRate;
        }
    }

    private void PlayFootStepSound()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 3f))
        {
            _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
            _audioSource.volume = _volume;
            _audioSource.PlayOneShot(_audioClips[Random.Range(0, _audioClips.Count)]);
        }
    }
}