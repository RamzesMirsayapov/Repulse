using UnityEngine;

public class CooldownAttackPowerUp : PowerUp
{
    [Header("Timing Settings")]
    [SerializeField, Min(0f)] private float _cooldownAttackMultiplier = 2f;
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