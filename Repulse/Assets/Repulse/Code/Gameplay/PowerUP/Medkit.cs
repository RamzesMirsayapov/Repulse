using UnityEngine;

public class Medkit : PowerUp
{
    [SerializeField] private float _healAmount;

    public override void Activate()
    {
        if (_player.TryGetComponent(out IHealable healable))
        {
            healable.ApplyHealth(_healAmount);
        }
    }
}
