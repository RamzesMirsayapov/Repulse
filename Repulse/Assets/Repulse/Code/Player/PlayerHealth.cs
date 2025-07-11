using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField, Min(0f)] private float _maxHealth;

    [SerializeField, Min(0f)] private int _blockCount;

    public void SetBlockCount(int newCount) => _blockCount = newCount;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        if (damage < 0)
            damage = 0;

        if (_blockCount > 0)
        {
            //вызов Ёффекта

            _blockCount--;

            return;
        }

        _currentHealth -= damage;
    }

    public void ApplyHealth(float heal)
    {
        if(heal < 0)
            heal = 0;

        _currentHealth += heal;
    }
}
