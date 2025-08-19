using System;
using UnityEngine;

[RequireComponent(typeof(Missile))]
public class DecoyExplosionAttack : MonoBehaviour, IDecoy
{
    public event Action OnExploded;

    public event Action OnDecoyExploded;

    [Header("Missile Settings")]
    [SerializeField] private Missile _missile;
    [SerializeField, Min(0f)] private float _damage;

    [Header("Overlap Settings")]
    [SerializeField] private OverlapSettings _overlapSettings;

    public void DecoyExplosiveAttack()
    {
        if (_overlapSettings.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);

            OnExploded?.Invoke();
        }

        DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _missile.UnRegisterPauseMissile();

        OnDecoyExploded?.Invoke();

        DestroyObject();
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
