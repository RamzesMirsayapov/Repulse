using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallisticMissileSpawner : MonoBehaviour
{
    [SerializeField] private float _missileSpeed = 40;

    [SerializeField] private List<Transform> _spawnPoints;

    private MissileCreator _missileCreator;

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

    private void SpawnMissile()
    {
        var newMissile = _missileCreator.CreateMissile(_missileSpeed, _spawnPoints[Random.Range(0, _spawnPoints.Count)]);
    }
}
