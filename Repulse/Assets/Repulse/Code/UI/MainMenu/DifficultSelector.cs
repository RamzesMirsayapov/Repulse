using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultSelector : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private DifficultyLevelConfig[] _levelConfigs;

    private void Start()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;

            _buttons[i].onClick.AddListener(() => OnLevelSelected(index));
        }
    }

    private void OnLevelSelected(int index)
    {
        SelectedConfigHolder.SelectedConfig = _levelConfigs[index];

        StartGame();
    }

    private void StartGame()
    {
        SceneLoader.LoadGameScene();
    }
}
