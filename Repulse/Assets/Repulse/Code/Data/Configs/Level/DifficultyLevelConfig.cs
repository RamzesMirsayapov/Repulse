using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyLevelConfig", menuName = "Config/LevelConfig/DifficultyLevelConfig")]
public class DifficultyLevelConfig : ScriptableObject
{
    [SerializeField] private List<LevelDifficultySettings> _levelDifficultySettings;

    public List<LevelDifficultySettings> LevelDifficultySettings => _levelDifficultySettings;
}
