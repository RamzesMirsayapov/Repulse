using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Explosion sounds")]
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _decoyExplosionSound;

    public void PlaySound(AudioClip audioClip, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(audioClip, position);
    }

    public void PlayExplosionSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_explosionSound, position);
    }

    public void PlayDecoyExplosionSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(_decoyExplosionSound, position);
    }
}
