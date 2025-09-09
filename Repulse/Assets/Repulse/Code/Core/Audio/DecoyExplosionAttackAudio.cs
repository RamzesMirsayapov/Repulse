using UnityEngine;

public class DecoyExplosionAttackAudio : BaseExplosionAudio
{
    [Header("Sounds")]
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _decoyExplosionSound;

    [Header("DecoyExplosionAttack")]
    [SerializeField] private DecoyExplosionAttack _decoyExplossionAttack;

    private void OnEnable()
    {
        _decoyExplossionAttack.OnExploded += PlayExplosionSound;
        _decoyExplossionAttack.OnDecoyExploded += PlayDecoyExplosionSound;
    }

    private void OnDisable()
    {
        _decoyExplossionAttack.OnExploded -= PlayExplosionSound;
        _decoyExplossionAttack.OnDecoyExploded -= PlayDecoyExplosionSound;
    }

    private void PlayExplosionSound() => PlaySound(_explosionSound);
    private void PlayDecoyExplosionSound() => PlaySound(_decoyExplosionSound);
}
