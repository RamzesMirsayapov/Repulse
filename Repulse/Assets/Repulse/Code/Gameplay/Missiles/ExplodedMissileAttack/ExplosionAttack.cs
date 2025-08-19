using System;
using UnityEngine;

[RequireComponent(typeof(Missile))]
public class ExplosionAttack : MonoBehaviour, IExplosion
{
    public event Action OnExploded;

    [SerializeField, Min(0f)] private float _damage;
    [SerializeField] private Missile _missile;

    [SerializeField] private OverlapSettings _overlapSettings;

    private void OnCollisionEnter(Collision collision)
    {
        _missile.UnRegisterPauseMissile();

        DestroyObject();
    }

    private void DestroyObject()
    {
        ExplosiveAttack();

        OnExploded?.Invoke();

        Destroy(gameObject);
    }

    private void ExplosiveAttack()
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

    private void PlayEffect()
    {

    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
