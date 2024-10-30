using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SpawnMissileSettings
{
    [SerializeField] private string _nameMissile;

    [SerializeField, Range(0f, 100f)] private float _chanceSpawn;

    [HideInInspector] public ISpawnable MissileCreator;

    [HideInInspector] public double Weight;

    public float ChanceSpawn => _chanceSpawn;
}
