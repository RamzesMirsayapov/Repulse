using UnityEngine;

public class DirectMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform transform)
    {
        var prefab = Resources.Load<GameObject>("Missiles/DefaultDirectMissile");

        var gameObject = GameObject.Instantiate(prefab, transform.position, transform.rotation);

        var missileComponent = gameObject.GetComponent<DirectMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
