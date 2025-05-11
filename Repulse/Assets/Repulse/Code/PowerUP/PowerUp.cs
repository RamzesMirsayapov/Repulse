using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PowerUp : MonoBehaviour
{
    protected LayerMask _playerMask;

    protected PowerUpSpawner _powerUPSpawner;

    protected Collision _collision;

    [Inject]
    private void Construct(PlayerMovement playerMovement, PowerUpSpawner powerUPSpawner)
    {
        _playerMask = playerMovement.gameObject.layer;

        _powerUPSpawner = powerUPSpawner;
    }

    public abstract void Activate();

    private void OnCollisionEnter(Collision collision)
    {
        if (_playerMask == collision.gameObject.layer)
        {
            _collision = collision;

            Activate();
        }
    }
}
