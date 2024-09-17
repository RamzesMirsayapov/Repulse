using UnityEngine;

public class ExplosionAttack : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _damage;

    [SerializeField] private OverlapSettings _overlapSettings;

    private OverlapTargetFinder _targetFinder;

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
        PerformAttack();

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
