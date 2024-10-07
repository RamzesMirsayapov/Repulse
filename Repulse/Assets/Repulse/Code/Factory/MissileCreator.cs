using UnityEngine;
using Zenject;

public abstract class MissileCreator
{
    [Inject] protected readonly DiContainer _diContainer;

    public abstract Missile CreateMissile(float speed, Transform spawnPoint);
}
