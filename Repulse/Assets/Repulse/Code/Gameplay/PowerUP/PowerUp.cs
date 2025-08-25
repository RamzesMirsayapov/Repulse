using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public abstract class PowerUp : MonoBehaviour
{
    public event Action<PowerUp> OnPickedUp;

    protected Player _player;

    protected Collider _collision;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    public abstract void Activate();

    private void OnTriggerEnter(Collider collider)
    {
        if(_player == null)
        {
            return;
        }

        if (_player.PlayerLayer == collider.gameObject.layer)
        {
            _collision = collider;

            OnPickedUp?.Invoke(this);

            Activate();

            Destroy(gameObject);
        }
    }
}
