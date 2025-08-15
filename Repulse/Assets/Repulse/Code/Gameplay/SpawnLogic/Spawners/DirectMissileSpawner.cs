using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DirectMissileSpawner : MissilesSpawner
{
    [SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;

    private List<ISpawnable> _missileCreators;

    private ProbabilityObjectSpawner _probabilitySpawnMissiles;

    private MissileCreator _directMissileCreator;
    private MissileCreator _decoyDirectMissileCreator;

    private int _randomSpawnPointValues;

    [Inject]
    private void Construct(DirectMissileCreator directMissileCreator, DecoyDirectMissileCreator decoyDirectMissileCreator)
    {
        _directMissileCreator = directMissileCreator;
        _decoyDirectMissileCreator = decoyDirectMissileCreator;

        InitializeSpawner();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            SpawnMissile(40);
        }
    }

    protected override void InitializeSpawner()
    {
        _missileCreators = new List<ISpawnable>() { _directMissileCreator, _decoyDirectMissileCreator };

        _probabilitySpawnMissiles = new ProbabilityObjectSpawner(_spawnObjectsSettings, _missileCreators);

        _probabilitySpawnMissiles.SortFactory();
    }

    public override void SpawnMissile(float speed)
    {
        _randomSpawnPointValues = Random.Range(0, _spawnPoints.Length);

        _missileCreator = (MissileCreator)_spawnObjectsSettings[_probabilitySpawnMissiles.GetRandomMissileIndex()].SpawnObject;

        var missile = _missileCreator.CreateMissile(speed, _spawnPoints[_randomSpawnPointValues]);
    }
}