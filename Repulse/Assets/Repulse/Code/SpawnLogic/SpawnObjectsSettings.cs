using System;
using UnityEngine;

[Serializable]
public class SpawnObjectsSettings
{
    [SerializeField] private string _nameObject;

    [SerializeField, Range(0f, 100f)] private float _chanceSpawn;

    [HideInInspector] public ISpawnable SpawnObject;

    [HideInInspector] public double Weight;

    public float ChanceSpawn => _chanceSpawn;
}
