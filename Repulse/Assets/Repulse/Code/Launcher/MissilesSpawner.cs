using System.Collections.Generic;
using UnityEngine;

public abstract class MissilesSpawner : MonoBehaviour, ISpawnable
{
    [SerializeField] protected float _speed = 40;

    [SerializeField] protected Transform[] _spawnPoints;

    protected MissileCreator _missileCreator;

    protected abstract void InitializeSpawner();
    public abstract void SpawnMissile();
}
