using UnityEngine;
using Zenject;

public class BallisticMissileSpawner : MissilesSpawner
{
    [Inject]
    private void Construct(BallisticMissileCreator ballisticMissileCreator)
    {
        _missileCreator = ballisticMissileCreator;
    }

    protected override void InitializeSpawner()
    {

    }

    public override void SpawnMissile(float speed)
    {
        var newMissile = _missileCreator.CreateMissile(speed, _spawnPoints[Random.Range(0, _spawnPoints.Length)]);
    }
}