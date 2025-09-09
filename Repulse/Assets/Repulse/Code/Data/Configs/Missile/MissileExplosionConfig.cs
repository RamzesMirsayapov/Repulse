using UnityEngine;

[CreateAssetMenu(menuName = "Config/Missile/ExplosionPreset", fileName = "ExplosionPreset")]
public class MissileExplosionConfig : ScriptableObject
{
    [Header("Damage")]
    [SerializeField] private float _damage;

    [Header("Overlap Settings")]
    [SerializeField] private OverlapSettings _overlapSettings;

    [Header("Particles")]
    [SerializeField] private ParticleSystem _decoyExplosionEffectPrefab;
    [SerializeField, Min(0)] private float _explosionEffectDestroyDelay;

    [Header("Audio")]
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _decoyExplosionSound;

    public float Damage => _damage;
    public OverlapSettings OverlapSettings => _overlapSettings;
    public ParticleSystem DecoyExplosionEffectPrefab => _decoyExplosionEffectPrefab;
    public float ExplosionEffectDestroyDelay => _explosionEffectDestroyDelay;
    public AudioClip ExplosionSound => _explosionSound;
    public AudioClip DecoyExplosionSound => _decoyExplosionSound;
}