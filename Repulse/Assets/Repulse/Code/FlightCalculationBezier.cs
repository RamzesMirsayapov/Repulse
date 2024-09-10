using UnityEngine;
using Zenject;

public class FlightCalculationBezier : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _middlePoint;
    [SerializeField] private Transform _targetPosition;

    private Vector3 _lastTargetPosition;

    private BallisticMissileSpawner _ballisticMissileSpawner;
    //private float value = 20;

    [Inject]
    private void Construct(PlayerMovement playerMovement, BallisticMissileSpawner ballisticMissileSpawner)
    {
        _targetPosition = playerMovement.transform;
        _ballisticMissileSpawner = ballisticMissileSpawner;
    }

    private void OnEnable()
    {
        _ballisticMissileSpawner.OnBallisticMissileSpawn += GetLastTargetPosition;
    }

    private void OnDisable()
    {
        _ballisticMissileSpawner.OnBallisticMissileSpawn -= GetLastTargetPosition;
    }

    public Vector3 GetPoint(float t)
    {
        float oneMinusT = 1f - t;

        t = Mathf.Clamp01(t);

        Vector3 formula = oneMinusT * oneMinusT * _startPoint.position + 2 * oneMinusT * t * _middlePoint.position + t * t * _lastTargetPosition;

        return formula;
    }

    public Vector3 GetFirstDerivative(float t)
    {
        float oneMinusT = 1f - t;

        t = Mathf.Clamp01(t);

        Vector3 formula = 2f * (1f - t) * (_middlePoint.position - _startPoint.position) + 2 * t * (_lastTargetPosition - _middlePoint.position);

        return formula;
    }

    private void GetLastTargetPosition()
    {
        _lastTargetPosition = _targetPosition.position;
    }

    //private void OnDrawGizmos()
    //{
    //    Vector3 previousPoint = _startPoint.position;

    //    for (int i = 0; i < value + 1; i++)
    //    {
    //        Vector3 point = GetPoint(i / value);

    //        Gizmos.DrawLine(previousPoint, point);

    //        previousPoint = point;
    //        //Gizmos.DrawWireSphere(GetFirstDerivative(i / value), 3f);
    //    }
    //}
}
