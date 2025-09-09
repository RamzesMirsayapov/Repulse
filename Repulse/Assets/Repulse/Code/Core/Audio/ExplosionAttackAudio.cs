using UnityEngine;

public class ExplosionAttackAudio : BaseExplosionAudio
{
    [Header("Sounds")]
    [SerializeField] private AudioClip _explosionSound;

    [Header("ExplosionAttack")]
    [SerializeField] private ExplosionAttack _explosionAttack;

    private void OnEnable() =>
        _explosionAttack.OnExploded += PlayExplosionSound;

    private void OnDisable() => 
        _explosionAttack.OnExploded -= PlayExplosionSound;

    private void PlayExplosionSound() => PlaySound(_explosionSound);
}
