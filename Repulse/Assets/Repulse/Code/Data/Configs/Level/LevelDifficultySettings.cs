using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelDifficultySettings
{
    [Header("Missile Settings")]
    [SerializeField] private float _speedMissiles;

    [Header("Spawn Settings")]
    [SerializeField] private float _cooldownSpawn;
    [SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;

    public List<SpawnObjectsSettings> SpawnObjectsSettings => _spawnObjectsSettings;
    public float CoolDownSpawn => _cooldownSpawn;
    public float SpeedMissiles => _speedMissiles;
}
