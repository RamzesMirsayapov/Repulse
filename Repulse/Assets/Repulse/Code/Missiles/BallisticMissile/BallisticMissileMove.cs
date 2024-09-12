using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BallisticMissileMove : Missile
{
    [SerializeField] private float _speedF = 0.1f;

    private float _flightTime = 0;

    private Vector3 _startPoint;
    private Vector3 _middlePoint;
    private Vector3 _lastTargetPosition;

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

    private void RotateMissile()
    {
        if (_flightCalculationBezier == null)
        {
            Debug.Log("ballisticMove 22 строчка дерьмоооо");
            return;
        }

        _flightTime += Time.deltaTime * _speedF;

        Vector3 rotation = _flightCalculationBezier.GetFirstDerivative(_startPoint, _middlePoint, _lastTargetPosition, _flightTime);

        Vector3 moveDirection = _flightCalculationBezier.GetPoint(_startPoint, _middlePoint, _lastTargetPosition, _flightTime);

        _rigidbody.MovePosition(moveDirection);

        _rigidbody.MoveRotation(Quaternion.LookRotation(rotation));
    }

    private void GetPoints()
    {
        _startPoint = _settingsCalculationBezier.StartPoint.position;
        _middlePoint = _settingsCalculationBezier.MiddlePoint.position;
        _lastTargetPosition = _settingsCalculationBezier.TargetPosition.position;
    }
}
