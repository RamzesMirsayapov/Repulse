using System;
using UnityEngine;
using UnityEngine.UI;

public class UIVictoryPanel : MonoBehaviour
{
    public event Action OnRestartButtonClicked;
    public event Action OnExitButtonClicked;

    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;

    [HideInInspector] public bool IsActive = false;

    private void OnEnable()
    {
        _restartButton?.onClick.AddListener(RestartButtonClick);
        _exitButton?.onClick.AddListener(ExitButtonClick);
    }

    private void OnDisable()
    {
        _restartButton?.onClick.RemoveListener(RestartButtonClick);
        _exitButton?.onClick.RemoveListener(ExitButtonClick);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);

        IsActive = active;
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