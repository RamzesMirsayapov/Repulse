using UnityEngine;

public abstract class MissilesSpawner : MonoBehaviour, ISpawnable
{
    [SerializeField] protected Transform[] _spawnPoints;

    protected MissileCreator _missileCreator;

    protected abstract void InitializeSpawner();
    public abstract void SpawnMissile(float speed);
}