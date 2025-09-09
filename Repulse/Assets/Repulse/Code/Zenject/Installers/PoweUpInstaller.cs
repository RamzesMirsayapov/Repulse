using UnityEngine;
using Zenject;

public class PoweUpInstaller : MonoInstaller
{
    [SerializeField] private PowerUpSpawner _powerUpSpawner;

    public override void InstallBindings()
    {
        BindPowerUpSpawner();
    }

    private void BindPowerUpSpawner()
    {
        Container.Bind<PowerUpSpawner>().FromInstance(_powerUpSpawner).AsSingle();
    }
}