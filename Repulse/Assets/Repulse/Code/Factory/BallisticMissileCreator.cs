using UnityEngine;

public class BallisticMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform transform)
    {
        var prefab = Resources.Load<GameObject>("Missiles/BallisticMissile");

        var gameObject = _diContainer.InstantiatePrefab(prefab, transform.position, transform.rotation, transform);

        var missileComponent = gameObject.GetComponent<BallisticMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
