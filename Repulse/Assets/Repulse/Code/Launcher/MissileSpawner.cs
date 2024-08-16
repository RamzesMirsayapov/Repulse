using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    private MissileCreator missileCreator;

    [SerializeField] private float _missileSpeed = 40;

    [SerializeField] private List<Transform> _spawnPoints;

    [SerializeField] private List<SpawnMissileSettings> _spawnMissileSettings;

    private List<MissileCreator> _missileCreators = new List<MissileCreator>() {
        new HomingMissileCreator(), new DirectMissileCreator()};

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    private void Start()
    {
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
