using UnityEngine;
using Zenject;

public class HomingMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform spawnPoint)
    {
        var prefab = Resources.Load<GameObject>("Missiles/DefaultHomingMissile");

        var gameObject = _diContainer.InstantiatePrefab(prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        var missileComponent = gameObject.GetComponent<HomingMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
