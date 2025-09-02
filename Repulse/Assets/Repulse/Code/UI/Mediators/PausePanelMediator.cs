using System;
using UnityEngine;
using Zenject;

public class PausePanelMediator : IDisposable
{
    private PauseManager _pauseManager;
    private UIPausePanel _pausePanel;
    private UIDefeatPanel _defeatPanel;
    private IInput _input;

    private bool _isOn = false;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager, UIPausePanel PausePanel, UIDefeatPanel defeatPanel)
    {
        _input = input;
        _pauseManager = pauseManager;
        _pausePanel = PausePanel;
        _defeatPanel = defeatPanel;

        _input.OnPause += CallPause;
        _pausePanel.OnResumeButtonClicked += CallPause;

        _pausePanel.OnRestartButtonClicked += UnPause;
        _pausePanel.OnExitButtonClicked += UnPause;
    }

    public void Dispose()
    {
        _input.OnPause -= CallPause;
        _pausePanel.OnResumeButtonClicked -= CallPause;

        _pausePanel.OnRestartButtonClicked -= UnPause;
        _pausePanel.OnExitButtonClicked -= UnPause;
    }

    private void UnPause()
    {
        _pauseManager.SetPaused(false);
    }

    private void CallPause()
    {
        if (_defeatPanel.IsActive)
            return;

        _isOn = !_isOn;

        _pauseManager.SetPaused(_isOn);

        _pausePanel.SetActive(_isOn);
    }
}
