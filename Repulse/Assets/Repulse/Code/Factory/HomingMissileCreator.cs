using UnityEngine;
using Zenject;

public class HomingMissileCreator : MissileCreator
{

    //private IInstantiator _instantiator;

    //public HomingMissileCreator(IInstantiator instantiator)
    //{
    //    _instantiator = instantiator;
    //}

    public override Missile CreateMissile(float speed, Transform transform)
    {
        Debug.Log(_diContainer == null); // ÎÍ ÂÑÅÃÄÀ ÂÛÄÀÀÅÒ NULL

        var prefab = Resources.Load<GameObject>("DefaultHomingMissile");

        //var gameObject = GameObject.Instantiate(prefab, transform.position, transform.rotation);

        var gameObject = _diContainer.InstantiatePrefab(prefab, transform.position, transform.rotation, null);

        var missileComponent = gameObject.GetComponent<HomingMissileMove>();

        missileComponent.Initialize(speed);

        return missileComponent;
    }
}
