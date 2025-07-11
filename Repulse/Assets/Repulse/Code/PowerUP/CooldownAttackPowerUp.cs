using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownAttackPowerUp : PowerUp
{
    [SerializeField, Min(0f)] private float _cooldownAttackMultiplier = 1.5f;
    [SerializeField, Min(0f)] private float _duration;

    private float _originalCooldownAttack;

    private WeaponAttack _weaponAttack => _player.WeaponAttack;

    public override void Activate()
    {
        _originalCooldownAttack = _weaponAttack.GetCooldownAttack();
        _weaponAttack.SetCooldownAttack(_originalCooldownAttack * _cooldownAttackMultiplier);

        StartCoroutine(ResetCooldownAttackAfterDelay());
    }

    private IEnumerator ResetCooldownAttackAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        _weaponAttack.SetCooldownAttack(_originalCooldownAttack);
    }
}
