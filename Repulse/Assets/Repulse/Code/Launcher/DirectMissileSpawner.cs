using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DirectMissileSpawner : MissilesSpawner
{
    [SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;

    private List<ISpawnable> _missileCreators;

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

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
            SpawnMissile();
        }
    }

    protected override void InitializeSpawner()
    {
        _missileCreators = new List<ISpawnable>() { _directMissileCreator, _decoyDirectMissileCreator };

        _probalitySpawnMissiles = new ProbalitySpawnMissiles(_spawnObjectsSettings, _missileCreators);

        _probalitySpawnMissiles.SortFactory();
    }

    public override void SpawnMissile()
    {
        _randomSpawnPointValues = Random.Range(0, _spawnPoints.Length);

        _missileCreator = (MissileCreator)_spawnObjectsSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].SpawnObject;

        var missile = _missileCreator.CreateMissile(_speed, _spawnPoints[_randomSpawnPointValues]);
    }
}
