using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMissilesSpawner : MonoBehaviour
{
    [SerializeField] private DifficultyLevelConfig _difficultyLevelConfig;

    private List<SpawnObjectsSettings> _spawnObjectsSettings => _difficultyLevelConfig.LevelDifficultySettings[0].SpawnObjectsSettings;
    private float _cooldownSpawn => _difficultyLevelConfig.LevelDifficultySettings[0].CoolDownSpawn;
    private float _speedMissile => _difficultyLevelConfig.LevelDifficultySettings[0].SpeedMissiles;

    public event Action OnMissileSpawn;

    [SerializeField] private List<MissilesSpawner> _missilesSpawners;

    //[SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;

    private List<ISpawnable> _missilesSpawner2 = new List<ISpawnable>();

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    private MissilesSpawner _missileSpawner;


    private Coroutine _spawnCoroutine;

    private float _spawnCooldown;

    private int _currentLevel;

    private void Start()
    {
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        foreach (var item in _missilesSpawners)
        {
            _missilesSpawner2.Add(item);
        }

        _probalitySpawnMissiles = new ProbalitySpawnMissiles(_spawnObjectsSettings, _missilesSpawner2);

        _probalitySpawnMissiles.SortFactory();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SpawnMissile();
        }
    }

    public void StartWork()
    {
        StopWork();
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopWork()
    {
        if (_spawnCoroutine != null)
            StopCoroutine(SpawnCoroutine());
    }

    public void SetLevel(int level)
    {
        _currentLevel = level;
    }

    private void SpawnMissile()
    {
        OnMissileSpawn?.Invoke();

        _missileSpawner = (MissilesSpawner)_spawnObjectsSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].SpawnObject;

        _missileSpawner.SpawnMissile();
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            SpawnMissile();

            yield return new WaitForSeconds(_spawnCooldown);
        }
    }
}
