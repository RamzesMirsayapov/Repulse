using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelDifficultySettings
{
    [SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;
    [SerializeField] private float _cooldownSpawn;
    [SerializeField] private float _speedMissiles;

    public List<SpawnObjectsSettings> SpawnObjectsSettings => _spawnObjectsSettings;
    public float CoolDownSpawn => _cooldownSpawn;
    public float SpeedMissiles => _speedMissiles;
}
