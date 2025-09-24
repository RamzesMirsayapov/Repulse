using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Level : MonoBehaviour
{
    public event Action OnLevelStarted;
    public event Action OnLevelFinished;

    [SerializeField] private GlobalMissilesSpawner _globalMissilesSpawner;

    [SerializeField] private float _breakTimeValue = 4f;

    private Timer _timer;

    private int _waveNumber = 0;

    [Inject]
    private void Construct(Timer timer)
    {
        _timer = timer;

        _timer.OnWaveCompleted += ChangeWave;
    }

    private void OnDisable()
    {
        _timer.OnWaveCompleted -= ChangeWave;
    }

    private void Start()
    {
        StartLevel();
    }

    private void StartLevel()
    {
        OnLevelStarted?.Invoke();

        ChangeWave();
    }

    private void ChangeWave()
    {
        if (_waveNumber >= 4)
        {
            WinLevel();
            return;
        }

        StartCoroutine(BreakBeforeNextWave());
    }

    private void WinLevel()
    {
        _globalMissilesSpawner.StopWork();

        OnLevelFinished?.Invoke();
    }

    private IEnumerator BreakBeforeNextWave()
    {
        _globalMissilesSpawner.StopWork();

        yield return new WaitForSeconds(_breakTimeValue);

        IncreaseWaveNumber();

    }

    private void IncreaseWaveNumber()
    {
        _globalMissilesSpawner.SetWave(_waveNumber);

        _waveNumber++;
    }
}