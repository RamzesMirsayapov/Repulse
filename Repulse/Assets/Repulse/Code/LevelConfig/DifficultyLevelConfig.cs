using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyLevelConfig", menuName = "LevelConfig/DifficultyLevelConfig")]
public class DifficultyLevelConfig : ScriptableObject
{
    [SerializeField] private List<LevelDifficultySettings> _levelDifficultySettings;

    public List<LevelDifficultySettings> LevelDifficultySettings => _levelDifficultySettings;

    //[SerializeField] private LevelDifficultySettings _firstWaveDifficultySettings;
    //[SerializeField] private LevelDifficultySettings _secondWaveDifficultySettings;
    //[SerializeField] private LevelDifficultySettings _thirdWaveDifficultySettings;
    //[SerializeField] private LevelDifficultySettings _fourthWaveDifficultySettings;

    //public LevelDifficultySettings FirstWaveDifficultySettings => _firstWaveDifficultySettings;
    //public LevelDifficultySettings SecondWaveDifficultySettings => _secondWaveDifficultySettings;
    //public LevelDifficultySettings ThirdWaveDifficultySettings => _thirdWaveDifficultySettings;
    //public LevelDifficultySettings FourthWaveDifficultySettings => _fourthWaveDifficultySettings;
}
