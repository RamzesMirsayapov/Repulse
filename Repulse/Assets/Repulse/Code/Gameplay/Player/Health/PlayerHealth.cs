using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    public event Action<float> OnHealthChanged;
    public event Action OnDamageReceived;
    public event Action OnPlayerDead;

    public event Action<Color> OnEffectStarted;
    public event Action OnEffectEnded;

    [SerializeField, Min(0f)] private float _maxHealth;

    private int _blockCount;

    private float _currentHealth;

    public float HealthNormalized => (float)_currentHealth / _maxHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            return;

        if (_blockCount > 0)
        {
            _blockCount--;

            return;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            Death();
        }

        OnHealthChanged?.Invoke(HealthNormalized);
        OnDamageReceived?.Invoke();
    }

    private void Death()
    {
        OnPlayerDead?.Invoke();
    }

    public void ApplyHealth(float heal)
    {
        if(heal < 0)
            heal = 0;

        _currentHealth += heal;

        if(_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        OnHealthChanged?.Invoke(HealthNormalized);
    }

    public void ApplyShieldEffect(int blockCount, float duration, Color boostEffectColor)
    {
        _blockCount = blockCount;

        OnEffectStarted?.Invoke(boostEffectColor);

        StartCoroutine(ResetShieldAfterDelay(duration, 0));
    }

    private IEnumerator ResetShieldAfterDelay(float duration, int blockCount)
    {
        yield return new WaitForSeconds(duration);

        OnEffectEnded?.Invoke();

        _blockCount = blockCount;
    }
}
