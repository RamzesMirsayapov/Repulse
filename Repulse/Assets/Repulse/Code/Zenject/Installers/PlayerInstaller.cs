using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Transform _playerSpawnPoint;

    private Player _player;

    private Camera _camera;

    public override void InstallBindings()
    {
        CreatePlayer();
        BindPlayer();
        BindPlayerCamera();

        LockCursor();
    }

    private void CreatePlayer()
    {
        _player = Container.
            InstantiatePrefabForComponent<Player>
            (_playerPrefab, _playerSpawnPoint.position,
            _playerSpawnPoint.rotation, null);
    }

    private void BindPlayer()
    {
        Container.
            Bind<Player>().
            FromInstance(_player).
            AsSingle();
    }

    private void BindPlayerCamera()
    {
        _camera = _player.gameObject.GetComponentInChildren<Camera>();

        Container.
            Bind<Camera>().
            FromInstance(_camera).
            AsSingle();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}