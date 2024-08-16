using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbalitySpawnMissiles
{
    private List<SpawnMissileSettings> _spawnMissileSettings;

    private List<MissileCreator> _missileCreators;

    private double _accumulatedWights;

    private System.Random _random = new System.Random();

    public ProbalitySpawnMissiles(List<SpawnMissileSettings> spawnMissileSettings, List<MissileCreator> missileCreators)
    {
        _spawnMissileSettings = spawnMissileSettings;
        _missileCreators = missileCreators;
    }

    public void SortFactory()
    {
        int index = 0;

        foreach (var settings in _spawnMissileSettings)
        {
            settings.MissileCreator = _missileCreators[index];

            index++;
        }
    }

    public int GetRandomMissileIndex()
    {
        CalculateWeights();

        double randomValue = _random.NextDouble() * _accumulatedWights;

        for (int i = 0; i < _spawnMissileSettings.Count; i++)
            if (_spawnMissileSettings[i].Weight >= randomValue)
                return i;

        return 0;
    }

    private void CalculateWeights()
    {
        _accumulatedWights = 0;

        foreach (var settings in _spawnMissileSettings)
        {
            _accumulatedWights += settings.ChanceSpawn;
            settings.Weight = _accumulatedWights;
        }
    }
}
