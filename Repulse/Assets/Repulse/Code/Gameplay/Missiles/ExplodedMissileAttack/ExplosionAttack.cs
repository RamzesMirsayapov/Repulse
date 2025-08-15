using System;
using UnityEngine;

[RequireComponent(typeof(Missile))]
public class ExplosionAttack : MonoBehaviour, IExplosion
{
    public event Action OnExploded;

    [SerializeField, Min(0f)] private float _damage;
    [SerializeField] private Missile _missile;

    [SerializeField] private OverlapSettings _overlapSettings;

    private void PerformAttack()
    {
        if(_missile.IsReflected)
        {
            return;
        }

        if (_overlapSettings.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
        }
    }

    private void DestroyObject()
    {
        PerformAttack();

        OnExploded?.Invoke();

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _missile.UnRegisterPauseMissile();

        DestroyObject();
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
