using UnityEngine;
using Zenject;

public class BallisticMissileMove : Missile
{
    [SerializeField] private float _speedF = 0.1f;

    private FlightCalculationBezier _flightCalculationBezier;

    private float value = 0;

    [Inject]
    private void Constuct(FlightCalculationBezier flightCalculationBezier)
    {
        _flightCalculationBezier = flightCalculationBezier;
    }

    public new void Initialize(float speed) //, FlightCalculationBezier flightCalculationBezier
    {
        base.Initialize(speed);
        //_flightCalculationBezier = flightCalculationBezier;
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

        value += Time.deltaTime * _speedF;

        Vector3 rotation = _flightCalculationBezier.GetFirstDerivative(value);

        Vector3 moveDirection = _flightCalculationBezier.GetPoint(value);

        _rigidbody.MovePosition(moveDirection);

        _rigidbody.MoveRotation(Quaternion.LookRotation(rotation));
    }
}
