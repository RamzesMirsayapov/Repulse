using System.Collections;
using UnityEngine;
using TMPro;
using Zenject;
using System;

public class Timer : MonoBehaviour, IPauseHandler
{
    public event Action<float> OnTimerUpdate;
    public event Action OnWaveCompleted;
    public event Action OnSpawnPowerUp;

    [SerializeField] private TMP_Text _timerText;

    private Level _level;

    private PauseManager _pauseManager;

    private Coroutine _timerCoroutine;

    private int _secondsElapsed = 0;

    private int _minutesElapsed = 0;

    private bool _isPaused;

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }

    [Inject]
    private void Construct(Level level, PauseManager pauseManager)
    {
        _level = level;
        _pauseManager = pauseManager;

        _level.OnWaveChanged += StartTimer;
        _level.OnLevelStarted += StartTimer;
        _level.OnLevelFinished += StopTimer;

        _pauseManager.Register(this);
        _isPaused = _pauseManager.IsPaused;
    }

    private void OnDisable()
    {
        _level.OnWaveChanged -= StartTimer;
        _level.OnLevelStarted -= StartTimer;
        _level.OnLevelFinished -= StopTimer;
    }

    public void StartTimer()
    {
        if(_timerCoroutine != null)
        {
            StopCoroutine(RunTimerCoroutine());
        }

        _secondsElapsed = 0;
        _minutesElapsed = 0;
        _timerCoroutine = StartCoroutine(RunTimerCoroutine());
    }

    public void StopTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(RunTimerCoroutine());
            _timerCoroutine = null;
        }
    }

    private IEnumerator RunTimerCoroutine()
    {
        while (true)
        {
            if (_isPaused)
            {
                yield return null;
                continue;
            }

            _secondsElapsed++;

            if (_secondsElapsed % 60 == 0)
            {
                _secondsElapsed = 0;
                _minutesElapsed++;

                OnWaveCompleted?.Invoke();
            }

            if (_secondsElapsed % 30 == 0)
            {
                OnSpawnPowerUp?.Invoke();
            }

            _timerText.text = _minutesElapsed.ToString("D2") + ":" + _secondsElapsed.ToString("D2");

            yield return new WaitForSeconds(1);
        }
    }
}
