using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeaponAttack : MonoBehaviour, IPauseHandler
{
    [Header("Common")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _rayCastMask;
    [SerializeField] private float _cooldawnAttack;

    [Header("Overlap")]
    [SerializeField] private OverlapSettings _overlapSettings;

    private PauseManager _pauseManager;
    private Transform cameraTransform => _mainCamera.transform;

    private readonly List<IReflectable> _reflectableResults = new(24);
    private readonly List<IDecoy> _decoyResults = new(24);

    private readonly float _distance = Mathf.Infinity;

    private bool _isWating = false;
    private bool _isPaused = false;

    private IInput _input;
    private Vector3 _endPointReflection;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager)
    {
        _input = input;
        _pauseManager = pauseManager;

        _input.OnLeftMouseClicked += PerformAttack;

        _pauseManager.Register(this);

        Cursor.lockState = CursorLockMode.Locked; ////убрать
    }

    private void OnDisable()
    {
        _input.OnLeftMouseClicked -= PerformAttack;
    }

    private void PerformAttack()
    {
        if (_isWating || _isPaused)
        {
            return;
        }

        //_overlapSettings.EffectPrefab.Play();

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

        StartCoroutine(CooldownAttack());
    }

    private void Reflect(IReflectable reflectable)
    {
        reflectable.ReflectionMove(_endPointReflection);
    }

    private void Decoy(IDecoy decoy)
    {
        decoy.DecoyExplosion();
    }

    private IEnumerator CooldownAttack()
    {
        _isWating = true;

        yield return new WaitForSeconds(_cooldawnAttack);

        _isWating = false;
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

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
