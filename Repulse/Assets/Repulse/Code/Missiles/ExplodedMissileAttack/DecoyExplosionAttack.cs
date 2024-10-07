using System;
using UnityEngine;

public class DecoyExplosionAttack : MonoBehaviour, IExplosion, IDecoy
{
    public event Action OnDie;

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
        DestroyObject();
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
