using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMissilesSpawner : MonoBehaviour
{
    //[SerializeField] private HomingMissileSpawner HomingLaunchers;
    //[SerializeField] private DirectMissileSpawner DirectLaunchers;
    //[SerializeField] private BallisticMissileSpawner BallisticLaunchers;

    public event Action OnMissileSpawn;

    [SerializeField] private List<MissilesSpawner> _missilesSpawner;

    [SerializeField] private List<SpawnMissileSettings> _spawnMissileSettings;

    private List<ISpawnable> _missilesSpawner2;

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    private MissilesSpawner _missileSpawner;

    private int _randomSpawnPointValues;

    private void Start()
    {
        InitializeSpawner();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SpawnMissile();
        }
    }

    private void InitializeSpawner()
    {


        _probalitySpawnMissiles = new ProbalitySpawnMissiles(_spawnMissileSettings, _missilesSpawner2);

        _probalitySpawnMissiles.SortFactory();
    }

    private void SpawnMissile()
    {
        OnMissileSpawn?.Invoke(); ///

        _randomSpawnPointValues = UnityEngine.Random.Range(0, _missilesSpawner.Count);

        _missileSpawner = (MissilesSpawner)_spawnMissileSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].MissileCreator;

        _missileSpawner.SpawnMissile();
    }
}
