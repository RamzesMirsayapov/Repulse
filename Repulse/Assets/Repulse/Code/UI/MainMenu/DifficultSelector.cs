using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class DifficultSelector : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;

    [SerializeField] private DifficultyLevelConfig[] _levelConfigs;

    [SerializeField] private List<GameObject> _levelCompletedInfo;

    private void Start()
    {
        Debug.Log("Easy -  " + YG2.saves.EasyLevelCompleted);
        Debug.Log("Normal -  " + YG2.saves.NormalLevelCompleted);
        Debug.Log("Hard -  " + YG2.saves.HighLevelCompleted);
        Debug.Log("Impossible -  " + YG2.saves.ImplossibleLevelCompleted);

        SubscribeButtonsToLevels();

        UpdateLevelCompletedInfo();
    }

    private void UpdateLevelCompletedInfo()
    {
        foreach (var obj in _levelCompletedInfo)
        {
            obj.SetActive(false);
        }

        if(YG2.saves.EasyLevelCompleted)
            _levelCompletedInfo[0].SetActive(true);

        if (YG2.saves.NormalLevelCompleted)
            _levelCompletedInfo[0].SetActive(true);

        if (YG2.saves.HighLevelCompleted)
            _levelCompletedInfo[0].SetActive(true);

        if (YG2.saves.ImplossibleLevelCompleted)
            _levelCompletedInfo[0].SetActive(true);
    }

    private void SubscribeButtonsToLevels()
    {
        for (int i = 0; i < _buttons.Length; i++)
        {
            int index = i;

            _buttons[i].onClick.AddListener(() => OnLevelSelected(index));
        }
    }

    private void OnLevelSelected(int index)
    {
        YG2.InterstitialAdvShow();

        SelectedConfigHolder.SelectedConfig = _levelConfigs[index];

        StartGame();
    }

    private void StartGame()
    {
        SceneLoader.LoadGameScene();
    }
}
