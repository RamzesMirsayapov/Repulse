using UnityEngine;

public class DecoyHomingMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform transform)
    {
        var prefab = Resources.Load<GameObject>("Missiles/DecoyMissilePrefabs/DecoyHomingMissile");

        var gameObject = _diContainer.InstantiatePrefab(prefab, transform.position, transform.rotation, null);

        var missileComponent = gameObject.GetComponent<HomingMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
