using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;

    [SerializeField] private Gradient _gradientHealthFilling;

    private PlayerHealth _playerHealth;

    [Inject]
    private void Construct(Player player)
    {
        _playerHealth = player.PlayerHealth;

        _playerHealth.OnHealthChanged += HealthBarChanchge;
    }

    private void OnDisable()
    {
        _playerHealth.OnHealthChanged -= HealthBarChanchge;
    }

    private void HealthBarChanchge(float valueAsPercentage)
    {
        _healthBarFilling.fillAmount = valueAsPercentage;

        _healthBarFilling.color = _gradientHealthFilling.Evaluate(valueAsPercentage);
    }
}
