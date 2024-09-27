using System;
using UnityEngine;

public class DecoyExplosionAttack : MonoBehaviour, IExplosion, IDecoy
{
    [SerializeField, Min(0f)] private float _damage;

    [SerializeField] private OverlapSettings _overlapSettings;

    private OverlapTargetFinder _targetFinder;

    public event Action OnDie;

    private void Start()
    {
        _targetFinder = new OverlapTargetFinder(_overlapSettings);
    }

    public void DecoyExplosion()
    {
        if (_targetFinder.TryFind(out IDamageable damageable))
        {
            damageable.ApplyDamage(_damage);
        }
        //вынести за условие
        DestroyObject();
    }

    private void DestroyObject()
    {
        OnDie?.Invoke();

        Debug.Log("уничтоение");
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestroyObject();
    }
}
