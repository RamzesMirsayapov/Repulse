using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private float _missileSpeed = 40;

    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private List<SpawnMissileSettings> _spawnMissileSettings;

    private List<MissileCreator> _missileCreators;

    private HomingMissileCreator _homingMissileCreator;

    private MissileCreator missileCreator;

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    [Inject]
    private void Construct(HomingMissileCreator homingMissileCreator)
    {
        _homingMissileCreator = homingMissileCreator;
    }

    private void Start()
    {
        _missileCreators = new List<MissileCreator>() {
        _homingMissileCreator, new DirectMissileCreator()};

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
        missileCreator = _spawnMissileSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].MissileCreator;
        
        var newMissile = missileCreator.CreateMissile(_missileSpeed, _spawnPoints[Random.Range(0,4)]);
    }
}
