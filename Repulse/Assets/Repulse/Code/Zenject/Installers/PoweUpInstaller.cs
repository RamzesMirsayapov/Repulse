using UnityEngine;
using Zenject;

public class PoweUpInstaller : MonoInstaller
{
    [SerializeField] private PowerUpSpawner _powerUpSpawner;

    public override void InstallBindings()
    {
        //BindPowerUp();

        BindPowerUpSpawner();
    }

    private void BindPowerUp()
    {
        Container.Bind<PowerUp>().AsSingle();
    }

    private void BindPowerUpSpawner()
    {
        Container.Bind<PowerUpSpawner>().FromInstance(_powerUpSpawner).AsSingle();
    }
}
