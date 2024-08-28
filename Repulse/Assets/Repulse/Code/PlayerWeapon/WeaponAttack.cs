using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private OverlapSettings _overlapSettings;

    [SerializeField] private LayerMask _rayCastMask;

    private OverlapTargetFinder _targetFinder;

    private float _distance = Mathf.Infinity;

    private Transform cameraTransform => _mainCamera.transform;

    private void Start()
    {
        _targetFinder = new OverlapTargetFinder(_overlapSettings);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PerformAttack();
        }
    }

    private void PerformAttack()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (_targetFinder.TryFind(out IReflectable damageable))
        {
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _rayCastMask))
            {
                damageable.ReflectionMove(hitInfo.point);
            }
        }
    }

    private void OnDrawGizmos()
    {
        _overlapSettings.TryDrawGizmos();

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _rayCastMask))
        {
            DrawRay(ray, hitInfo.point, hitInfo.distance, Color.red);
        }
        else
        {
            var hitPosition = ray.origin + ray.direction * _distance;

            DrawRay(ray, hitPosition, _distance, Color.green);
        }
    }

    private void DrawRay(Ray ray, Vector3 hitPosition, float distance, Color color)
    {
        const float hitPointRadius = 0.25f;

        Debug.DrawRay(ray.origin, ray.direction * distance, color);

        Gizmos.color = color;
        Gizmos.DrawSphere(hitPosition, hitPointRadius);
    }
}