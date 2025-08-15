using UnityEngine;
using Zenject;

public class BallisticMissileMove : Missile
{
    [SerializeField] private float _speedF = 0.1f;  

    private float _flightTime = 0;

    private const int _indexMaskEverything = 1;

    private Vector3 _startPoint, _middlePoint, _lastTargetPosition, _targetPosition;

    private FlightCalculationBezier _flightCalculationBezier;
    private SettingsCalculationBezier _settingsCalculationBezier;

    [Inject]
    private void Constuct(FlightCalculationBezier flightCalculationBezier,
        SettingsCalculationBezier settingsCalculationBezier)
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
        if (_isPaused)
            return;

        base.Move();

        if (!IsReflected)
            MoveMissile();
    }

    private void GetPoints()
    {
        _startPoint = _settingsCalculationBezier.StartPoint.position;
        _middlePoint = _settingsCalculationBezier.MiddlePoint.position;
        _lastTargetPosition = _settingsCalculationBezier.TargetPosition.position;

        Ray ray = new Ray(_lastTargetPosition, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000f, _indexMaskEverything))
        {
            _targetPosition = hitInfo.point;
        }
    }

    private void RotateMissile()
    {
        Vector3 rotation = _flightCalculationBezier.GetFirstDerivative(_startPoint, _middlePoint, _targetPosition, _flightTime);

        _rigidbody.MoveRotation(Quaternion.LookRotation(rotation));
    }

    private void MoveDirectionMissile()
    {
        Vector3 moveDirection = _flightCalculationBezier.GetPoint(_startPoint, _middlePoint, _targetPosition, _flightTime);

        _rigidbody.MovePosition(moveDirection);
    }

    private void MoveMissile()
    {
        _flightTime += Time.deltaTime * _speedF;

        RotateMissile();

        MoveDirectionMissile();
    }
}
