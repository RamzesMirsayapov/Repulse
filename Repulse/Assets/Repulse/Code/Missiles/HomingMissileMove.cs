using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class HomingMissileMove : Missile
{
    [SerializeField] private float _rotateSpeed = 200f;

    [Header("PREDICTION")]
    [SerializeField] private float _maxDistancePredict = 100f;
    [SerializeField] private float _minDistancePredict = 20f;
    [SerializeField] private float _maxTimePrediction = 0.1f;
    private Vector3 _standardPrediction, _deviatedPrediction;

    [Header("DEVIATION")]
    [SerializeField] private float _deviationAmount = 25f;
    [SerializeField] private float _deviationSpeed = 2f;

    private PlayerMovement _playerTarget;

    [Inject]
    private void Constuct(PlayerMovement player)
    {
        _playerTarget = player;
    }

    public new void Initialize(float speed)
    {
        base.Initialize(speed);
    }

    private void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        base.Move();

        if(!IsReflected)
        {
            var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _playerTarget.transform.position));

            PredictMovement(leadTimePercentage);

            AddDeviation(leadTimePercentage);

            RotateRocket();
        }
    }

    private void PredictMovement(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);

        _standardPrediction = _playerTarget.transform.position + _playerTarget.CharacterController.velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * _deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * _deviationAmount * leadTimePercentage;

        _deviatedPrediction = _standardPrediction + predictionOffset;
    }

    private void RotateRocket()
    {
        var heading = _deviatedPrediction - transform.position;
        var rotation = Quaternion.LookRotation(heading);

        _rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _standardPrediction);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(_standardPrediction, _deviatedPrediction);
    }
}
