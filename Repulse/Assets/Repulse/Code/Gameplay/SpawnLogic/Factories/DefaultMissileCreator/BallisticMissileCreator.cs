using UnityEngine;

public class BallisticMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform spawnPoint)
    {
        var prefab = Resources.Load<GameObject>("Missiles/BallisticMissile");

        var gameObject = _diContainer.InstantiatePrefab(prefab, spawnPoint.position, spawnPoint.rotation, spawnPoint);

        var missileComponent = gameObject.GetComponent<BallisticMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
