using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HomingMissileSpawner : MonoBehaviour
{
    [SerializeField] private float _missileSpeed = 40;

    [SerializeField] private Transform[] _spawnPoints;

    [SerializeField] private List<SpawnMissileSettings> _spawnMissileSettings;

    private List<MissileCreator> _missileCreators;
    private HomingMissileCreator _homingMissileCreator;
    private DecoyHomingMissileCreator _decoyHomingMissileCreator;
    private MissileCreator _missileCreator;

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    private int _randomSpawnPointValues;

    [Inject]
    private void Construct(HomingMissileCreator homingMissileCreator, DecoyHomingMissileCreator decoyHomingMissileCreator)
    {
        _homingMissileCreator = homingMissileCreator;
        _decoyHomingMissileCreator = decoyHomingMissileCreator;
    }

    private void Start()
    {
        _missileCreators = new List<MissileCreator>() { _homingMissileCreator, _decoyHomingMissileCreator };

        _probalitySpawnMissiles = new ProbalitySpawnMissiles(_spawnMissileSettings, _missileCreators);

        _probalitySpawnMissiles.SortFactory();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            SpawnMissile();
        }
    }

    private void SpawnMissile()
    {
        _randomSpawnPointValues = Random.Range(0, _spawnPoints.Length);

        _missileCreator = _spawnMissileSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].MissileCreator;
        
        var newMissile = _missileCreator.CreateMissile(_missileSpeed, _spawnPoints[_randomSpawnPointValues]);
    }
}
