using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GlobalMissilesSpawner _globalMissilesSpawner;

    public event Action OnLevelStarted;
    public event Action OnLevelFinished;

    private int _waveNumber = 0;

    private void Start()
    {
        Invoke("RestartLevel", 4f);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            ChangeWave();
        }
    }

    private void RestartLevel()
    {
        OnLevelStarted?.Invoke();

        _globalMissilesSpawner.SetWave(_waveNumber);
        _globalMissilesSpawner.StopWork();
        _globalMissilesSpawner.StartWork();
    }

    private void ChangeWave()
    {
        if (_waveNumber >= 4)
        {
            OnLevelFinished?.Invoke();
            return;
        }

        _waveNumber++;

        _globalMissilesSpawner.SetWave(_waveNumber);
        _globalMissilesSpawner.StartWork();
    }

    private void LoseLevel()
    {
        _globalMissilesSpawner.StopWork();

        OnLevelFinished?.Invoke();
    }
}
