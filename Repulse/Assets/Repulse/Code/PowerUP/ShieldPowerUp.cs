using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    [SerializeField, Min(0f)] private int _blockCount;

    [SerializeField, Min(0f)] private float _duration;

    private PlayerHealth _playerHealth => _player.PlayerHealth;

    public override void Activate()
    {
        _playerHealth.SetBlockCount(_blockCount);

        StartCoroutine(ResetBlockCountAfterDelay());
    }

    private IEnumerator ResetBlockCountAfterDelay()
    {
        yield return new WaitForSeconds(_duration);

        _playerHealth.SetBlockCount(0);
    }
}
