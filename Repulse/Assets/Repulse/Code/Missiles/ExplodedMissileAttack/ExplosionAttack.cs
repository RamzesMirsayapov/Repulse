using System;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour, IExplosion
{
    public event Action OnDie;

    [SerializeField, Min(0f)] private float _damage;

    [SerializeField] private OverlapSettings _overlapSettings;

    private void PerformAttack()
    {
        if (_overlapSettings.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
        }
    }

    private void DestroyObject()
    {
        PerformAttack();

        OnDie?.Invoke();

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyObject();
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
