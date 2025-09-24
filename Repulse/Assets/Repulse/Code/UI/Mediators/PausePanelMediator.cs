using System;
using Zenject;
using YG;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelMediator : IDisposable
{
    private PauseManager _pauseManager;
    private UIPausePanel _pausePanel;
    private UIDefeatPanel _defeatPanel;
    private UIVictoryPanel _victoryPanel;
    private IInput _input;

    private bool _isOn = false;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager, UIPausePanel PausePanel, UIDefeatPanel defeatPanel, UIVictoryPanel victoryPanel)
    {
        _input = input;
        _pauseManager = pauseManager;
        _pausePanel = PausePanel;
        _defeatPanel = defeatPanel;
        _victoryPanel = victoryPanel;

        _input.OnPause += CallPause;
        _pausePanel.OnResumeButtonClicked += CallPause;

        _pausePanel.OnRestartButtonClicked += UnPause;
        _pausePanel.OnExitButtonClicked += UnPause;

        if(YG2.nowInterAdv == false)
        {
            _pauseManager.SetPaused(false);
        }
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

        YG2.InterstitialAdvShow();
    }


    private void CallPause()
    {
        if (_defeatPanel.IsActive || _victoryPanel.IsActive)
            return;

        if (YG2.nowInterAdv)
            return;

        _isOn = !_isOn;

        _pauseManager.SetPaused(_isOn);

        _pausePanel.SetActive(_isOn);

        if (_isOn)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}