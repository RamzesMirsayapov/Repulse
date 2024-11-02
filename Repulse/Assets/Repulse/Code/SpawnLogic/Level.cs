using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GlobalMissilesSpawner _globalMissilesSpawner;

    public event Action OnLevelStarted;
    public event Action OnLevelFinished;

    private int _levelNumber = 0;

    private void RestartLevel()
    {
        OnLevelStarted?.Invoke();

        _globalMissilesSpawner.SetLevel(_levelNumber);
        _globalMissilesSpawner.StopWork();
        _globalMissilesSpawner.StartWork();
    }


    private void ChangeLevel()
    {
        _levelNumber++;

        _globalMissilesSpawner.SetLevel(_levelNumber);
        _globalMissilesSpawner.StartWork();
    }

    private void LoseLevel()
    {
        _globalMissilesSpawner.StopWork();

        OnLevelFinished?.Invoke();
    }
}
