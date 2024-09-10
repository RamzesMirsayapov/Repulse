using UnityEngine;
using Zenject;

public class HomingMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform transform)
    {
        var prefab = Resources.Load<GameObject>("Missiles/DefaultHomingMissile");

        var gameObject = _diContainer.InstantiatePrefab(prefab, transform.position, transform.rotation, null);

        var missileComponent = gameObject.GetComponent<HomingMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
