using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

[RequireComponent(typeof(Missile))]
public class DecoyExplosionAttack : MonoBehaviour, IExplosion, IDecoy
{
    public event Action OnExploded;

    public event Action OnDecoyExploded;

    [Header("Missile Settings")]
    [SerializeField] private Missile _missile;
    [SerializeField, Min(0f)] private float _damage;

    [Header("Overlap Settings")]
    [SerializeField] private OverlapSettings _overlapSettings;

    [Header("DecoyExplosionEffect")]
    [SerializeField] private ParticleSystem _decoyExplosionEffect;

    private SoundManager _soundManager;

    [Inject]
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }

    public void DecoyExplosiveAttack()
    {
        _soundManager.PlayExplosionSound(transform.position);
        SpawnEffectOnDestroy(_overlapSettings.EffectPrefab);

        if (_overlapSettings.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);

            OnExploded?.Invoke();
        }

        DestroyObject();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _missile.UnRegisterPauseMissile();

        OnDecoyExploded?.Invoke();

        _soundManager.PlayDecoyExplosionSound(transform.position);

        SpawnEffectOnDestroy(_decoyExplosionEffect);

        DestroyObject();
    }

    private void SpawnEffectOnDestroy(ParticleSystem particle)
    {
        var effect = Instantiate(particle, transform.position, Quaternion.Euler(Vector3.up));

        effect.Play();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
