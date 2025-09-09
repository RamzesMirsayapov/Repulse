using UnityEngine;
using Zenject;

public class SettingsCalculationBezier : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _middlePoint;

    private Transform _targetPosition;

    public Transform StartPoint => _startPoint;
    public Transform MiddlePoint => _middlePoint;
    public Transform TargetPosition => _targetPosition;

    [Inject]
    private void Construct(Player player)
    {
        _targetPosition = player.transform;
    }
}