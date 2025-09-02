using System;
using Zenject;

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
        _pauseManager.SetPaused(false);
    }

    private void CallVictoryPanel()
    {
        if (_pausePanel.IsActive)
            _pausePanel.SetActive(false);

        _pauseManager.SetPaused(true);

        _victoryPanel.SetActive(true);
    }
}
