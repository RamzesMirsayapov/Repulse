using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GlobalMissilesSpawner : MonoBehaviour, IPauseHandler
{
    [SerializeField] private List<MissilesSpawner> _missileSpawners;

    private DifficultyLevelConfig _difficultyLevelConfig;
    private List<SpawnObjectsSettings> _spawnObjectsSettings;
    private float _spawnCooldown;
    private float _missileSpeed;
    private int _currentWave;
    private Coroutine _spawnCoroutine;
    private bool _isPaused;

    private PauseManager _pauseManager;
    private ProbabilityObjectSpawner _probabilitySpawner;

    private MissilesSpawner _missileSpawner;

    [Inject]
    private void Construct(DifficultyLevelConfig levelDifficultySettings, PauseManager pauseManager)
    {
        _difficultyLevelConfig = levelDifficultySettings;

        if (_difficultyLevelConfig == null)
        {
            _difficultyLevelConfig = Resources.Load<DifficultyLevelConfig>("LevelConfig/NormalDifficultyLevelConfig");
        }

        _pauseManager = pauseManager;
        _isPaused = _pauseManager.IsPaused;

        UpdateSpawnerSettings();
    }

    public void StartWork()
    {
        StopWork();
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    public void StopWork()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    public void SetWave(int wave)
    {
        _currentWave = wave;

        UpdateSpawnerSettings();

        StartWork();
    }

    private void UpdateSpawnerSettings()
    {
        var waveSettings = _difficultyLevelConfig.LevelDifficultySettings[_currentWave];

        _spawnObjectsSettings = waveSettings.SpawnObjectsSettings;
        _spawnCooldown = waveSettings.CoolDownSpawn;

        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        _probabilitySpawner = new ProbabilityObjectSpawner(
            _spawnObjectsSettings,
            _missileSpawners.Cast<ISpawnable>().ToList()
        );

        _probabilitySpawner.SortFactory();
    }

    private void SpawnMissile()
    {
        var spawnSetting = _spawnObjectsSettings[_probabilitySpawner.GetRandomMissileIndex()];
        var selectedSpawner = (MissilesSpawner)spawnSetting.SpawnObject;

        selectedSpawner.SpawnMissile(spawnSetting.MissileSpeed);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            if (_isPaused)
            {
                yield return null;
                continue;
            }

            SpawnMissile();

            yield return new WaitForSeconds(_spawnCooldown);
        }
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }
}