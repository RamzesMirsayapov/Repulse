using UnityEngine;

public class Medkit : PowerUp
{
    [SerializeField] private float _healAmount;

    public override void Activate()
    {
        //_powerUPSpawner.RemoveMedkitFromList(this);

        if(_collision.gameObject.TryGetComponent(out IHealable healable))
        {
            healable.ApplyHealth(_healAmount);
        }
    }
}
