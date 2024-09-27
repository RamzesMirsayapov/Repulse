using System;
using UnityEngine;

public class ExplosionAttack : MonoBehaviour, IExplosion
{
    [SerializeField, Min(0f)] private float _damage;

    [SerializeField] private OverlapSettings _overlapSettings;

    private OverlapTargetFinder _targetFinder;

    public event Action OnDie;

    private void Start()
    {
        _targetFinder = new OverlapTargetFinder(_overlapSettings);
    }

    private void PerformAttack()
    {
        if (_targetFinder.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyObject();
    }

    private void DestroyObject()
    {
        PerformAttack();

        OnDie?.Invoke();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
