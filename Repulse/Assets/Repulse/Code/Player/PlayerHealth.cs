using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IHealable
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        //Debug.Log("нанесло урона:    " + damage);

        _currentHealth -= damage;
    }

    public void ApplyHealth(float heal)
    {
        _currentHealth += heal;
    }
}
