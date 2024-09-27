using UnityEngine;

public class DirectMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform transform)
    {
        var prefab = Resources.Load<GameObject>("Missiles/DefaultDirectMissile");

        var gameObject = _diContainer.InstantiatePrefab(prefab, transform.position, transform.rotation, null);

        var missileComponent = gameObject.GetComponent<DirectMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
