using UnityEngine;

public class ShieldPowerUp : PowerUp
{
    [Header("Shield Settings")]
    [SerializeField, Min(0)] private int _blockCount;
    [SerializeField, Min(0f)] private float _duration;

    [Header("BoostEffectColor")]
    [SerializeField] private Color _boostEffectColor = Color.blue;

    private PlayerHealth _playerHealth => _player.PlayerHealth;

    public override void Activate()
    {
        _playerHealth.ApplyShieldEffect(_blockCount, _duration, _boostEffectColor);
    }
}