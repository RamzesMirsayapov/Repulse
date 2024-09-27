using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    [SerializeField] private OverlapSettings _overlapSettings;

    [SerializeField] private LayerMask _rayCastMask;

    private Transform cameraTransform => _mainCamera.transform;

    private OverlapTargetFinder _targetFinder;

    private const float _distance = Mathf.Infinity;

    private Vector3 _endPointReflection;

    private readonly List<IReflectable> _reflectableResults = new(24);
    private readonly List<IDecoy> _decoyResults = new(24);

    private void Start()
    {
        _targetFinder = new OverlapTargetFinder(_overlapSettings);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            test();
            PerformAttack();
        }
    }

    private void PerformAttack()
    {

        if (_targetFinder.TryFind(_reflectableResults))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _rayCastMask))
            {
                _endPointReflection = hitInfo.point;
                _reflectableResults.ForEach(Reflect);
            }
        }
    }

    private void test()
    {
        if (_targetFinder.TryFind(_decoyResults))
        {
            Debug.Log("dgsgdsgdsgdgd");

            _decoyResults.ForEach(Decoy);

            //return;
        }
    }

    private void Reflect(IReflectable reflectable)
    {
        reflectable.ReflectionMove(_endPointReflection);
    }

    private void Decoy(IDecoy decoy)
    {
        decoy.DecoyExplosion();
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
