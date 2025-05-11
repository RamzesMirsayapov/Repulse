using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private PlayerJumpHandler _jumpHandler;

    [SerializeField] private PlayerHealth _playerHealth;

    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerJumpHandler PlayerJumpHandler => _jumpHandler;
    public PlayerHealth PlayerHealth => _playerHealth;
}
