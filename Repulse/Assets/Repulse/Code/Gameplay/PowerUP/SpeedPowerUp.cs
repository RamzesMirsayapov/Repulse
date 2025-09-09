using UnityEngine;

public class SpeedPowerUP : PowerUp
{
    [Header("Speed Settings")]
    [SerializeField, Min(0f)] private float _speedMultiplier = 1.5f;
    [SerializeField, Min(0f)] private float _duration;

    [Header("BoostEffectColor")]
    [SerializeField] private Color _boostEffectColor = Color.blue;

    private float _originalSpeed;

    private PlayerMovement _playerMovement => _player.PlayerMovement;

    public override void Activate()
    {
        _playerMovement.ApplySpeedEffect(_speedMultiplier, _duration, _boostEffectColor);
    }
}