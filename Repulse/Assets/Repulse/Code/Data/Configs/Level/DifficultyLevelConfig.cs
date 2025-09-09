using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyLevelConfig", menuName = "Config/LevelConfig/DifficultyLevelConfig")]
public class DifficultyLevelConfig : ScriptableObject
{
    [SerializeField] private string _configId;

    [SerializeField] private List<LevelDifficultySettings> _levelDifficultySettings;

    public string ConfigId => _configId;
    public List<LevelDifficultySettings> LevelDifficultySettings => _levelDifficultySettings;
}