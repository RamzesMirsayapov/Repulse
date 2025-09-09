using System;
using Zenject;
using YG;
using UnityEngine;

public class VictoryPanelMediator : IDisposable
{
    private PauseManager _pauseManager;
    private UIVictoryPanel _victoryPanel;
    private UIPausePanel _pausePanel;
    private Level _level;

    [Inject]
    private void Construct(Level level, PauseManager pauseManager, UIVictoryPanel victoryPanel, UIPausePanel PausePanel)
    {
        _level = level;
        _pauseManager = pauseManager;
        _victoryPanel = victoryPanel;
        _pausePanel = PausePanel;

        _level.OnLevelFinished += CallVictoryPanel;

        _victoryPanel.OnRestartButtonClicked += UnPause;
        _victoryPanel.OnExitButtonClicked += UnPause;
    }

    public void Dispose()
    {
        _level.OnLevelFinished -= CallVictoryPanel;

        _victoryPanel.OnRestartButtonClicked -= UnPause;
        _victoryPanel.OnExitButtonClicked -= UnPause;
    }

    private void UnPause()
    {
        YG2.InterstitialAdvShow();
        
        _pauseManager.SetPaused(false);
    }

    private void CallVictoryPanel()
    {
        if (_pausePanel.IsActive)
            _pausePanel.SetActive(false);

        SetLevelCompleted();

        _pauseManager.SetPaused(true);

        _victoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void SetLevelCompleted()
    {
        switch (SelectedConfigHolder.SelectedConfig.ConfigId)
        {
            case "easy":
                YG2.saves.EasyLevelCompleted = true;
                break;

            case "normal":
                YG2.saves.NormalLevelCompleted = true;
                break;

            case "high":
                YG2.saves.HighLevelCompleted = true;
                break;

            case "impossible":
                YG2.saves.ImplossibleLevelCompleted = true;
                break;
        }

        YG2.SaveProgress();
    }
}