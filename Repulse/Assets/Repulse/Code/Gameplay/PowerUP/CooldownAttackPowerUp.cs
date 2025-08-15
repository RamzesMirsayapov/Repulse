using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownAttackPowerUp : PowerUp
{
    [Header("Timing Settings")]
    [SerializeField, Min(0f)] private float _cooldownAttackMultiplier = 1.5f;
    [SerializeField, Min(0f)] private float _duration;

    [Header("BoostEffectColor")]
    [SerializeField] private Color _boostEffectColor = Color.yellow;

    private float _originalCooldownAttack;

    private WeaponAttack _weaponAttack => _player.WeaponAttack;

    public override void Activate()
    {
        _weaponAttack.ApplyCooldownAttackEffect(_cooldownAttackMultiplier, _duration, _boostEffectColor);
    }
}
