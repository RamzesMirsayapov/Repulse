using System;
using UnityEngine;
using Zenject;

public class PausePanelMediator : MonoBehaviour, IDisposable
{
    private PauseManager _pauseManager;
    private IInput _input;

    private bool _isOn = false;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager)
    {
        _pauseManager = pauseManager;
        _input = input;

        _input.OnPause += CallPause;
    }

    public void Dispose()
    {
        _input.OnPause -= CallPause;
    }

    private void CallPause()
    {
        _isOn = !_isOn;

        _pauseManager.SetPaused(_isOn);
    }
}
