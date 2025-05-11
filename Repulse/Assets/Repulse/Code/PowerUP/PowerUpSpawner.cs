using ModestTree;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private PowerUp _medkit; // сменить тип класса

    [SerializeField] private List<PowerUp> _powerUPs;

    [SerializeField] private List<Transform> _spawnPoints;

    private List<PowerUp> _usedPowerUPs;

    private List<PowerUp> _usedMedkits;

    private Timer _timer;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;

        _timer.OnSpawnPowerUp += SpawnPowerUP;
        _timer.OnWaveCompleted += SpawnMedkit;
    }

    private void SpawnPowerUP()
    {
        if (_usedPowerUPs.IsEmpty())
        {
            Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;

            _usedPowerUPs.Add(Instantiate(_powerUPs[Random.Range(0, _powerUPs.Count)], spawnPosition, Quaternion.identity));
        }
    }

    private void SpawnMedkit()
    {
        if (_usedMedkits.IsEmpty())
        {
            Vector3 spawnPosition = _spawnPoints[Random.Range(0, _spawnPoints.Count)].position;

            _usedMedkits.Add(Instantiate(_medkit, spawnPosition, Quaternion.identity));
        }
    }

    public void RemovePowerUpPFromList(PowerUp powerUp)
    {
        _usedMedkits.Remove(powerUp);
    }

    public void RemoveMedkitFromList(PowerUp medkit)
    {
        _usedMedkits.Remove(medkit);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(_playerMask == collision.gameObject.layer)
    //    {

    //    }
    //}
}
