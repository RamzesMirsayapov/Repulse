using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BallisticMissileMove : Missile
{
    [SerializeField] private float _speedF = 0.1f;

    private float _flightTime = 0;

    private const int _indexMaskEverything = 1;

    private Vector3 _startPoint;
    private Vector3 _middlePoint;
    private Vector3 _lastTargetPosition;

    private Vector3 _targetPosition;

    private FlightCalculationBezier _flightCalculationBezier;
    private SettingsCalculationBezier _settingsCalculationBezier;

    [Inject]
    private void Constuct(FlightCalculationBezier flightCalculationBezier, SettingsCalculationBezier settingsCalculationBezier)
    {
        _flightCalculationBezier = flightCalculationBezier;
        _settingsCalculationBezier = settingsCalculationBezier;
    }

    public new void Initialize(float speed)
    {
        base.Initialize(speed);

        GetPoints();
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        base.Move();

        if(!IsReflected)
            RotateMissile();
    }

    private void GetPoints()
    {
        _startPoint = _settingsCalculationBezier.StartPoint.position;
        _middlePoint = _settingsCalculationBezier.MiddlePoint.position;
        _lastTargetPosition = _settingsCalculationBezier.TargetPosition.position;

        Ray ray = new Ray(_lastTargetPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, _indexMaskEverything))
        {
            _targetPosition = hitInfo.point;
        }
    }

    private void RotateMissile()
    {
        _flightTime += Time.deltaTime * _speedF;

        Vector3 rotation = _flightCalculationBezier.GetFirstDerivative(_startPoint, _middlePoint, _targetPosition, _flightTime);

        Vector3 moveDirection = _flightCalculationBezier.GetPoint(_startPoint, _middlePoint, _targetPosition, _flightTime);

        _rigidbody.MovePosition(moveDirection);

        _rigidbody.MoveRotation(Quaternion.LookRotation(rotation));
    }
}
