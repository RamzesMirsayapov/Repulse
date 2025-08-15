using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using Zenject;

public class ShakeCameraOnWeaponAttack : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private ShakePositionRotationSettings _weaponAttackShakeSettings;

    [SerializeField] private ShakePositionRotationSettings _explosionShakeSettings;

    private Transform _cameraTransform;

    private void OnEnable()
    {
        _cameraTransform = _player.Camera.transform;

        _player.WeaponAttack.OnAttack += WeaponShakeCamera;
        _player.PlayerHealth.OnDamageReceived += ExplosionShakeCamera;
    }

    private void OnDisable()
    {
        _player.WeaponAttack.OnAttack -= WeaponShakeCamera;
        _player.PlayerHealth.OnDamageReceived -= ExplosionShakeCamera;
    }

    private void WeaponShakeCamera()
    {
        ShakeCamera(_weaponAttackShakeSettings);
    }
    
    private void ExplosionShakeCamera()
    {
        ShakeCamera(_explosionShakeSettings);
    }

    private void ShakeCamera(ShakePositionRotationSettings shakeSettings)
    {
        ShakePosition(shakeSettings.PositionShake);
        ShakeRotation(shakeSettings.RotationShake);
    }

    private void ShakePosition(ShakeAnimationPreset preset)
    {
        _cameraTransform
            .DOShakePosition(preset.Duration, preset.Strength, preset.Vibrato, preset.Randomness, preset.Snapping, true, preset.RandomnessMode)
            .SetEase(preset.Ease)
            .SetLink(_cameraTransform.gameObject);
    }

    private void ShakeRotation(ShakeAnimationPreset preset)
    {
        _cameraTransform
            .DOShakeRotation(preset.Duration, preset.Strength, preset.Vibrato, preset.Randomness, preset.Snapping, preset.RandomnessMode)
            .SetEase(preset.Ease)
            .SetLink(_cameraTransform.gameObject);
    }
}
