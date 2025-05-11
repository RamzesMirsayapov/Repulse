using System;
using UnityEngine;
using UnityEngine.UI;

public class UIPausePanel : MonoBehaviour
{
    public event Action OnResumeButtonClicked;
    public event Action OnRestartButtonClicked;
    public event Action OnExitButtonClicked;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    private void OnEnable()
    {
        _resumeButton?.onClick.AddListener(ResumeButtonClick);
        _restartButton?.onClick.AddListener(RestartButtonClick);
        _exitButton?.onClick.AddListener(ExitButtonClick);
    }

    private void OnDisable()
    {
        _resumeButton?.onClick.RemoveListener(ResumeButtonClick);
        _restartButton?.onClick.RemoveListener(RestartButtonClick);
        _exitButton?.onClick.RemoveListener(ExitButtonClick);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void ResumeButtonClick()
    {
        OnResumeButtonClicked?.Invoke();
    }

    private void RestartButtonClick()
    {
        OnRestartButtonClicked?.Invoke();
    }

    private void ExitButtonClick()
    {
        OnExitButtonClicked?.Invoke();
    }
}
