using UnityEngine;

public class ExplodeAttack : MonoBehaviour
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
        //if (_explosionPrefab) Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        //if (collision.transform.TryGetComponent<IExplode>(out var ex)) ex.Explode();

        PerformAttack();

        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();
    }
}
