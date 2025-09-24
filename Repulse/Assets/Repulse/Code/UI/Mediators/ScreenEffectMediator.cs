using System;
using UnityEngine;
using Zenject;

public class ScreenEffectMediator : IDisposable
{
    private Player _player;

    private ScreenEffectController _edgeGlowController;

    [Inject]
    private void Construct(Player player, ScreenEffectController edgeGlowController)
    {
        _player = player;
        _edgeGlowController = edgeGlowController;

        _player.PlayerHealth.OnEffectStarted += ActiveEffect;
        _player.PlayerMovement.OnEffectStarted += ActiveEffect;
        _player.WeaponAttack.OnEffectStarted += ActiveEffect;

        _player.PlayerHealth.OnEffectEnded += ResetEffect;
        _player.PlayerMovement.OnEffectEnded += ResetEffect;
        _player.WeaponAttack.OnEffectEnded += ResetEffect;
    }

    public void Dispose()
    {
        _player.PlayerHealth.OnEffectStarted -= ActiveEffect;
        _player.PlayerMovement.OnEffectStarted -= ActiveEffect;
        _player.WeaponAttack.OnEffectStarted -= ActiveEffect;

        _player.PlayerHealth.OnEffectEnded -= ResetEffect;
        _player.PlayerMovement.OnEffectEnded -= ResetEffect;
        _player.WeaponAttack.OnEffectEnded -= ResetEffect;
    }

    private void ActiveEffect(Color boostEffectColor)
    {
        _edgeGlowController.SetGlow(boostEffectColor);
    }

    private void ResetEffect()
    {
        _edgeGlowController.ResetGlow();
    }
}