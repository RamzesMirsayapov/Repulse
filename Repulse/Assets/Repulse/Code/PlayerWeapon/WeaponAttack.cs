using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _rayCastMask;

    [SerializeField] private OverlapSettings _overlapSettings;

    [SerializeField] private Camera _mainCamera;
    private Transform cameraTransform => _mainCamera.transform;

    private readonly List<IReflectable> _reflectableResults = new(24);
    private readonly List<IDecoy> _decoyResults = new(24);

    private Vector3 _endPointReflection;

    private readonly float _distance = Mathf.Infinity;

    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;

        _input.OnLeftMouseClicked += PerformAttack;

        Cursor.lockState = CursorLockMode.Locked; ////убрать
    }

    private void OnDisable()
    {
        _input.OnLeftMouseClicked -= PerformAttack;
    }

    private void PerformAttack()
    {
        if (_overlapSettings.TryFind(_decoyResults))
        {
            _decoyResults.ForEach(Decoy);
        }

        if (_overlapSettings.TryFind(_reflectableResults))
        {
            Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _distance, _rayCastMask))
            {
                _endPointReflection = hitInfo.point;
                _reflectableResults.ForEach(Reflect);
            }
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
