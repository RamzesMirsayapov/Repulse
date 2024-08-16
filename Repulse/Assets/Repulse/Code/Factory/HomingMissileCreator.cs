using UnityEngine;

public class HomingMissileCreator : MissileCreator
{
    public override Missile CreateMissile(float speed, Transform transform)
    {
        var prefab = Resources.Load<GameObject>("DefaultHomingMissile");

        var gameObject = GameObject.Instantiate(prefab, transform.position, transform.rotation);

        var missileComponent = gameObject.GetComponent<HomingMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
