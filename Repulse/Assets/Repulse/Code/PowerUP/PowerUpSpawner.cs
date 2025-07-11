using ModestTree;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private Medkit _medkitPrefab; // сменить тип класса

    [SerializeField] private List<PowerUp> _powerUpPrefabs;

    [SerializeField] private List<Transform> _spawnPoints;

    //private List<PowerUp> _usedPowerUPs;

    //private List<PowerUp> _usedMedkits;

    private PowerUp _activeMedkit;
    private PowerUp _activePowerUp;

    private List<Transform> _occupiedPoints = new();

    private Timer _timer;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;

        //_timer.OnSpawnPowerUp += SpawnPowerUP;
        //_timer.OnWaveCompleted += SpawnMedkit;

        _timer.OnSpawnPowerUp += TrySpawnPowerUp;
        _timer.OnWaveCompleted += TrySpawnMedkit;
    }

    private void OnDisable()
    {
        _timer.OnSpawnPowerUp -= TrySpawnPowerUp;
        _timer.OnWaveCompleted -= TrySpawnMedkit;
    }

    private void TrySpawnPowerUp()
    {
        if (_activePowerUp != null)
            return;

        if (TryGetFreeSpawnPoint(out var spawnPoint))
        {
            _activePowerUp = Instantiate(_powerUpPrefabs[Random.Range(0, _powerUpPrefabs.Count)], spawnPoint.position, Quaternion.identity);
            SetupPowerUp(_activePowerUp, spawnPoint);
        }
    }

    private void TrySpawnMedkit()
    {
        if (_activeMedkit != null)
            return;

        if (TryGetFreeSpawnPoint(out var spawnPoint))
        {
            _activeMedkit = Instantiate(_medkitPrefab, spawnPoint.position, Quaternion.identity);
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
                _activePowerUp = null;
        };
    }

    //private void SpawnPowerUP()
    //{
    //    if (_usedPowerUPs.IsEmpty())
    //    {
    //        Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;

    //        _usedPowerUPs.Add(Instantiate(_powerUPs[Random.Range(0, _powerUPs.Count)], spawnPosition, Quaternion.identity));
    //    }
    //}

    //private void SpawnMedkit()
    //{
    //    if (_usedMedkits.IsEmpty())
    //    {
    //        Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;

    //        _usedMedkits.Add(Instantiate(_medkit, spawnPosition, Quaternion.identity));
    //    }
    //}

    //public void RemovePowerUpPFromList(PowerUp powerUp)
    //{
    //    _usedMedkits.Remove(powerUp);
    //}

    //public void RemoveMedkitFromList(PowerUp medkit)
    //{
    //    _usedMedkits.Remove(medkit);
    //}
}
