using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    public event Action<float> OnDamageApplied;
    public event Action OnDamageReceived;

    public event Action<Color> OnEffectStarted;
    public event Action OnEffectEnded;

    [SerializeField, Min(0f)] private float _maxHealth;

    private int _blockCount;

    private float _currentHealth;

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

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
        {
            Debug.Log("отрицательный урон!!!");
            return;
        }

        if (_blockCount > 0)
        {
            //вызов Ёффекта

            _blockCount--;

            return;
        }

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            Death();
        }

        float currentHealthAsPercentage = _currentHealth / _maxHealth;

        OnDamageApplied?.Invoke(currentHealthAsPercentage);
        OnDamageReceived?.Invoke();
    }

    private void Death()
    {

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
    }
}
