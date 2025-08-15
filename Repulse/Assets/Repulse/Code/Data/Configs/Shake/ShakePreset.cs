using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakePreset : MonoBehaviour
{
    [SerializeField] private ShakePositionRotationSettings _shakeSettings;

    public ShakePositionRotationSettings ShakeSettings => _shakeSettings;
}
