using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class DirectMissileMove : Missile
{
    public new void Initialize(float speed) => base.Initialize(speed);

    private void FixedUpdate() => Move();

    protected override void Move()
    {
        if (_isPaused)
            return;

        base.Move();
    }
}
