using System;
using Zenject;
using YG;
using UnityEngine;

public class DefeatPanelMediator : IDisposable
{
    private PauseManager _pauseManager;
    private UIDefeatPanel _defeatPanel;
    private UIPausePanel _pausePanel;
    private Player _player;

    [Inject]
    private void Construct(Player player, PauseManager pauseManager, UIDefeatPanel defeatPanel, UIPausePanel PausePanel)
    {
        _player = player;
        _pauseManager = pauseManager;
        _defeatPanel = defeatPanel;
        _pausePanel = PausePanel;

        _player.PlayerHealth.OnPlayerDead += CallDefeatPanel;

        _defeatPanel.OnRestartButtonClicked += UnPause;
        _defeatPanel.OnExitButtonClicked += UnPause;
    }

    public void Dispose()
    {
        _player.PlayerHealth.OnPlayerDead -= CallDefeatPanel;

        _defeatPanel.OnRestartButtonClicked -= UnPause;
        _defeatPanel.OnExitButtonClicked -= UnPause;
    }

    private void UnPause()
    {
        _pauseManager.SetPaused(false);

        YG2.InterstitialAdvShow();
    }

    private void CallDefeatPanel()
    {
        if (_pausePanel.IsActive)
            _pausePanel.SetActive(false);

        _pauseManager.SetPaused(true);

        _defeatPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}