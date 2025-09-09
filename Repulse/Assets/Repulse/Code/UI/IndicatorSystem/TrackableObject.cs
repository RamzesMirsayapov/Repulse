using UnityEngine;
using Zenject;

public class TrackableObject : MonoBehaviour
{
    [SerializeField] private Missile _missile;

    [Header("Indicator Settings")]
    [SerializeField] private bool _registerOnStart = true;
    [SerializeField] private Sprite _indicatorIcon;


    private IndicatorSystem _indicatorSystem;

    [Inject]
    private void Construct(IndicatorSystem indicatorSystem)
    {
        _indicatorSystem = indicatorSystem;

        _missile.OnReflected += Unregister;
    }

    private void OnEnable()
    {
        if (_registerOnStart)
        {
            Register();
        }
    }

    private void OnDisable()
    {
        _missile.OnReflected -= Unregister;

        if (_indicatorSystem != null)
        {
            Unregister();
        }
    }

    public void Register()
    {
        _indicatorSystem.RegisterObject(transform, _indicatorIcon);
    }

    public void Unregister()
    {
        _indicatorSystem.UnregisterObject(transform);
    }
}
