using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovement _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    [SerializeField] private Missile _homingNissilePrefab;

    private PlayerMovement playerMovement;

    public override void InstallBindings()
    {
        CreatePlayer();

        BindMissile();

        BindHomingMissileCreator();
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
}