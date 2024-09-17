using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovement _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    private PlayerMovement playerMovement;

    public override void InstallBindings()
    {
        CreatePlayer();

        BindMissile();

        BindHomingMissileCreator();

        BindBallisticMissile();
    }

    private void CreatePlayer()
    {
        playerMovement = Container.
            InstantiatePrefabForComponent<PlayerMovement>
            (_playerPrefab, _playerSpawnPoint.position,
            _playerSpawnPoint.rotation, null);
    }

    private void BindMissile()
    {
        Container.
            Bind<PlayerMovement>().
            FromInstance(playerMovement).
            AsSingle();
    }

    private void BindHomingMissileCreator()
    {
        Container.
            Bind<HomingMissileCreator>().
            AsSingle();
    }

    private void BindBallisticMissile()
    {
        Container.Bind<BallisticMissileCreator>().AsSingle();

        Container.Bind<FlightCalculationBezier>().FromNew().AsTransient();

        Container.Bind<SettingsCalculationBezier>().FromComponentInParents().AsTransient();
    }
}