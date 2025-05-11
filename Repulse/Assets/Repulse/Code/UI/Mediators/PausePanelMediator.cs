using System;
using UnityEngine;
using Zenject;

public class PausePanelMediator : IDisposable
{
    private UIPausePanel _pausePanel;
    private PauseManager _pauseManager;
    private IInput _input;

    private bool _isOn = false;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager, UIPausePanel PausePanel)
    {
        _pausePanel = PausePanel;
        _pauseManager = pauseManager;
        _input = input;

        _input.OnPause += CallPause;
        _pausePanel.OnResumeButtonClicked += CallPause;
    }

    public void Dispose()
    {
        _input.OnPause -= CallPause;
    }

    private void CallPause()
    {
        _isOn = !_isOn;

        _pauseManager.SetPaused(_isOn);

        _pausePanel.SetActive(_isOn);
    }
}
