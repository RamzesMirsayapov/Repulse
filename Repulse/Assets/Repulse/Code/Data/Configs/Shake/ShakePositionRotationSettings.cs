using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Camera/Shake Preset", fileName = "Shake Preset")]
public class ShakePositionRotationSettings : ScriptableObject
{
    [SerializeField] private ShakeAnimationPreset _positionShake;
    [SerializeField] private ShakeAnimationPreset _rotationShake;

    public ShakeAnimationPreset PositionShake => _positionShake;
    public ShakeAnimationPreset RotationShake => _rotationShake;
}
