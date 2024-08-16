using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyDamage(float damage)
    {
        //if(damage <= 0)
        //    damage = 0;

        Debug.Log("нанесло урона:    " + damage);

        _currentHealth -= damage;
    }
}
