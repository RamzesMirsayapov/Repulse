using System;
using UnityEngine;

[RequireComponent(typeof(Missile))]
public class DecoyExplosionAttack : MonoBehaviour, IExplosion, IDecoy
{
    public event Action OnDie;

    [SerializeField] private Missile _missile;

    [SerializeField, Min(0f)] private float _damage;

    [SerializeField] private OverlapSettings _overlapSettings;

    public void DecoyExplosion()
    {
        if (_overlapSettings.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
        }

        DestroyObject();
    }

    private void DestroyObject()
    {
        OnDie?.Invoke();

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
