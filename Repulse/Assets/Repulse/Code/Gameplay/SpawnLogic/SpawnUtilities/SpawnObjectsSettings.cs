using System;
using UnityEngine;

[Serializable]
public class SpawnObjectsSettings
{
    [SerializeField] private string _nameObject;

    [SerializeField, Range(0f, 100f)] private float _chanceSpawn;

    [SerializeField, Min(0f)] private float _missleSpeed;

    [HideInInspector] public ISpawnable SpawnObject;

    [HideInInspector] public double Weight;

    public float ChanceSpawn => _chanceSpawn;
    public float MissileSpeed => _missleSpeed;
}
