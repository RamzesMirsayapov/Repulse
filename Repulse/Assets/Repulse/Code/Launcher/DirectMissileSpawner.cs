using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DirectMissileSpawner : MonoBehaviour
{
    [SerializeField] private float _speed = 40;

    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private List<SpawnMissileSettings> _spawnMissileSettings;

    private int _randomSpawnPointValues;

    private List<MissileCreator> _missileCreators;
    private MissileCreator _directMissileCreator;
    private MissileCreator _decoyDirectMissileCreator;
    private MissileCreator _missileCreator;

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    [Inject]
    private void Construct(DirectMissileCreator directMissileCreator, DecoyDirectMissileCreator decoyDirectMissileCreator)
    {
        _directMissileCreator = directMissileCreator;
        _decoyDirectMissileCreator = decoyDirectMissileCreator;
    }

    private void Start() => InitializeSpawner();

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            SpawnMissile();
        }
    }

    private void SpawnMissile()
    {
        _randomSpawnPointValues = Random.Range(0, _spawnPoints.Length);

        _missileCreator = _spawnMissileSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].MissileCreator;

        var missile = _missileCreator.CreateMissile(_speed, _spawnPoints[_randomSpawnPointValues]);
    }

    private void InitializeSpawner()
    {
        _missileCreators = new List<MissileCreator>() { _directMissileCreator, _decoyDirectMissileCreator };

        _probalitySpawnMissiles = new ProbalitySpawnMissiles(_spawnMissileSettings, _missileCreators);

        _probalitySpawnMissiles.SortFactory();
    }
}
