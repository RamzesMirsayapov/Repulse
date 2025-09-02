using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Missile))]
public class ExplosionAttack : MonoBehaviour, IExplosion
{
    public event Action OnExploded;

    [Header("Missile Settings")]
    [SerializeField, Min(0f)] private float _damage;
    [SerializeField] private Missile _missile;

    [Header("Overlap Settings")]
    [SerializeField] private OverlapSettings _overlapSettings;

    private SoundManager _soundManager;

    [Inject]
    private void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ExplosiveAttack();

        OnExploded?.Invoke();

        _missile.UnRegisterPauseMissile();

        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void ExplosiveAttack()
    {
        _soundManager.PlayExplosionSound(transform.position);

        SpawnEffectOnDestroy();

        if (_missile.IsReflected)
        {
            return;
        }

        if (_overlapSettings.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
        }
    }

    private void SpawnEffectOnDestroy()
    {
        var effect = Instantiate(_overlapSettings.EffectPrefab, transform.position, Quaternion.Euler(Vector3.up));

        effect.Play();
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
