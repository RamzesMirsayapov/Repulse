using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HomingMissileSpawner : MissilesSpawner
{
    [SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;

    private List<ISpawnable> _missileCreators;
    private HomingMissileCreator _homingMissileCreator;
    private DecoyHomingMissileCreator _decoyHomingMissileCreator;

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    private int _randomSpawnPointValues;

    [Inject]
    private void Construct(HomingMissileCreator homingMissileCreator, DecoyHomingMissileCreator decoyHomingMissileCreator)
    {
        _homingMissileCreator = homingMissileCreator;
        _decoyHomingMissileCreator = decoyHomingMissileCreator;

        InitializeSpawner();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            SpawnMissile(40);
        }
    }

    protected override void InitializeSpawner()
    {
        _missileCreators = new List<ISpawnable>() { _homingMissileCreator, _decoyHomingMissileCreator };

        _probalitySpawnMissiles = new ProbalitySpawnMissiles(_spawnObjectsSettings, _missileCreators);

        _probalitySpawnMissiles.SortFactory();
    }

    public override void SpawnMissile(float speed)
    {
        _randomSpawnPointValues = Random.Range(0, _spawnPoints.Length);

        _missileCreator = (MissileCreator)_spawnObjectsSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].SpawnObject;
        
        var newMissile = _missileCreator.CreateMissile(speed, _spawnPoints[_randomSpawnPointValues]);
    }
}
