using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelDifficultySettings
{
    [Header("Spawn Settings")]
    [SerializeField] private float _cooldownSpawn;
    [SerializeField] private List<SpawnObjectsSettings> _spawnObjectsSettings;

    public List<SpawnObjectsSettings> SpawnObjectsSettings => _spawnObjectsSettings;
    public float CoolDownSpawn => _cooldownSpawn;
}
