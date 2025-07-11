using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private PlayerJumpHandler _jumpHandler;

    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private WeaponAttack _weaponAttack;

    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerJumpHandler PlayerJumpHandler => _jumpHandler;
    public PlayerHealth PlayerHealth => _playerHealth;
    public CharacterController CharacterController => _characterController;
    public WeaponAttack WeaponAttack => _weaponAttack;

    public Vector3 PlayerPosition => gameObject.transform.position;
    public LayerMask PlayerMask => gameObject.layer;

    private void Start()
    {
        
    }
}
