using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float _cooldownSpawnPowerUp = 30f;

    [Header("PowerUp Prefabs")]
    [SerializeField] private Medkit _medkitPrefab;
    [SerializeField] private List<PowerUp> _powerUpPrefabs;

    [Header("Spawn Positions")]
    [SerializeField] private List<Transform> _spawnPoints;

    [Inject] protected readonly DiContainer _diContainer;

    private PowerUp _activeMedkit;
    private PowerUp _activePowerUp;

    private List<Transform> _occupiedPoints = new();

    private Timer _timer;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;

        Initialize();
    }

    private void Initialize()
    {
        TrySpawnPowerUp();

        _timer.OnWaveCompleted += TrySpawnMedkit;
    }

    private void OnDisable()
    {
        _timer.OnWaveCompleted -= TrySpawnMedkit;
    }

    private void TrySpawnPowerUp()
    {
        if (_activePowerUp != null)
            return;

        if (TryGetFreeSpawnPoint(out var spawnPoint))
        {
            var prefab = _powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Count)];

            var gameobject = _diContainer.InstantiatePrefab(prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

            if (!gameobject.TryGetComponent(out PowerUp PowerUpComponent))
            {
                return;
            }

            _activePowerUp = PowerUpComponent;
            SetupPowerUp(_activePowerUp, spawnPoint);
        }
        else
        {
            Debug.Log("Нет свободных точек спавна!");
        }
    }

    private void TrySpawnMedkit()
    {
        if (_activeMedkit != null)
            return;

        if (TryGetFreeSpawnPoint(out var spawnPoint))
        {
            var gameObject = _diContainer.InstantiatePrefab(_medkitPrefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

            var PowerUpComponent = gameObject.GetComponent<PowerUp>();

            _activeMedkit = PowerUpComponent;

            SetupPowerUp(_activeMedkit, spawnPoint);
        }
    }

    private bool TryGetFreeSpawnPoint(out Transform spawnPoint)
    {
        var freePoints = _spawnPoints.Where(point => !_occupiedPoints.Contains(point)).ToList();

        if (freePoints.Count > 0)
        {
            spawnPoint = freePoints[Random.Range(0, freePoints.Count)];
            return true;
        }

        spawnPoint = null;
        return false;
    }

    private void SetupPowerUp(PowerUp powerUp, Transform spawnPoint)
    {
        _occupiedPoints.Add(spawnPoint);

        powerUp.OnPickedUp += (p) =>
        {
            _occupiedPoints.Remove(spawnPoint);

            if (powerUp is Medkit)
                _activeMedkit = null;
            else
            {
                _activePowerUp = null;

                StartCoroutine(SpawnPowerUp());
            }    
        };
    }

    private IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(_cooldownSpawnPowerUp);

        TrySpawnPowerUp();
    }
}