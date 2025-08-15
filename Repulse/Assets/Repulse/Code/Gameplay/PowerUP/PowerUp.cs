using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PowerUp : MonoBehaviour
{
    public event Action<PowerUp> OnPickedUp;

    protected Player _player;

    protected Collision _collision;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    public abstract void Activate();

    private void OnCollisionEnter(Collision collision)
    {
        if(_player == null)
        {
            return;
        }

        if (_player.PlayerLayer == collision.gameObject.layer)
        {
            _collision = collision;

            OnPickedUp?.Invoke(this);

            Activate();

            Destroy(gameObject);
        }
    }
}
