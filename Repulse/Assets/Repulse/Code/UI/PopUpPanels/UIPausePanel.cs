using System;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UIPausePanel : MonoBehaviour
{
    public event Action OnResumeButtonClicked;
    public event Action OnRestartButtonClicked;
    public event Action OnExitButtonClicked;

    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    public bool IsActive = false;

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

        IsActive = active;
    }

    private void ResumeButtonClick()
    {
        OnResumeButtonClicked?.Invoke();
    }

    private void RestartButtonClick()
    {
        OnRestartButtonClicked?.Invoke();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneLoader.LoadGameScene();
    }

    private void ExitButtonClick()
    {
        OnExitButtonClicked?.Invoke();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneLoader.LoadMainMenu();
    }
}