using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUP : PowerUp
{
    [SerializeField, Min(0f)] private float _speedMultiplier = 1.5f;
    [SerializeField, Min(0f)] private float _duration;

    private float _originalSpeed;

    private PlayerMovement _playerMovement => _player.PlayerMovement;

    public override void Activate()
    {
        _originalSpeed = _playerMovement.GetSpeed();
        _playerMovement.SetSpeed(_originalSpeed * _speedMultiplier);

        StartCoroutine(ResetSpeedAfterDelay());
    }

    private IEnumerator ResetSpeedAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        _playerMovement.SetSpeed(_originalSpeed);
    }
}
