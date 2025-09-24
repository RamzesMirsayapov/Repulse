using UnityEngine;

public class ShakePreset : MonoBehaviour
{
    [SerializeField] private ShakePositionRotationSettings _shakeSettings;

    public ShakePositionRotationSettings ShakeSettings => _shakeSettings;
}