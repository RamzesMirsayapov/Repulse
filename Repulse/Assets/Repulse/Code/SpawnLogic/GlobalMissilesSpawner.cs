using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalMissilesSpawner : MonoBehaviour, IPauseHandler
{
    [SerializeField] private DifficultyLevelConfig _difficultyLevelConfig;

    [SerializeField] private List<MissilesSpawner> _missilesSpawners;

    private List<SpawnObjectsSettings> _spawnObjectsSettings;

    private float _cooldownSpawn;
    private float _speedMissile;

    public event Action OnMissileSpawn;

    private List<ISpawnable> _missilesSpawner2 = new List<ISpawnable>();  ////нзвание сменить

    private ProbalitySpawnMissiles _probalitySpawnMissiles;

    private MissilesSpawner _missileSpawner;

    private Coroutine _spawnCoroutine;

    private PauseManager _pauseManager;

    private bool _isPaused;

    private int _currentWave;

    [Inject]
    private void Construct(PauseManager pauseManager)
    {
        _pauseManager = pauseManager;

        _isPaused = _pauseManager.IsPaused;
    }

    private void Start()
    {
        UpdateObjectSpawnerSettings();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            SpawnMissile();
        }
    }

    private void UpdateObjectSpawnerSettings()
    {
        UpdateConfigValues();
        InitializeSpawner();
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

    public void SetWave(int wave)
    {
        _currentWave = wave;

        UpdateObjectSpawnerSettings();
    }

    private void UpdateConfigValues()
    {
        _spawnObjectsSettings = _difficultyLevelConfig.LevelDifficultySettings[_currentWave].SpawnObjectsSettings;

        _cooldownSpawn = _difficultyLevelConfig.LevelDifficultySettings[_currentWave].CoolDownSpawn;
        _speedMissile = _difficultyLevelConfig.LevelDifficultySettings[_currentWave].SpeedMissiles;
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

    private void SpawnMissile()
    {
        OnMissileSpawn?.Invoke();

        _missileSpawner = (MissilesSpawner)_spawnObjectsSettings[_probalitySpawnMissiles.GetRandomMissileIndex()].SpawnObject;

        _missileSpawner.SpawnMissile(_speedMissile);
    }

    private IEnumerator SpawnCoroutine()
    {
        if (_isPaused)
            yield return null;

        while(true)
        {
            SpawnMissile();

            yield return new WaitForSeconds(_cooldownSpawn);
        }
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }
}
