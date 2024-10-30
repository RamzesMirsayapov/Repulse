using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallisticMissileSpawner : MissilesSpawner
{
    [Inject]
    private void Construct(BallisticMissileCreator ballisticMissileCreator)
    {
        _missileCreator = ballisticMissileCreator;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.V))
        {
            SpawnMissile();
        }
    }

    protected override void InitializeSpawner()
    {

    }

    public override void SpawnMissile()
    {
        var newMissile = _missileCreator.CreateMissile(_speed, _spawnPoints[Random.Range(0, _spawnPoints.Length)]);
    }
}
