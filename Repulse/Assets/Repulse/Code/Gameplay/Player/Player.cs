using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    [SerializeField] private PlayerJumpHandler _jumpHandler;

    [SerializeField] private PlayerHealth _playerHealth;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private WeaponAttack _weaponAttack;

    [SerializeField] private Camera _camera;

    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerJumpHandler PlayerJumpHandler => _jumpHandler;
    public PlayerHealth PlayerHealth => _playerHealth;
    public CharacterController CharacterController => _characterController;
    public WeaponAttack WeaponAttack => _weaponAttack;
    public Camera Camera => _camera;

    public Vector3 PlayerPosition => gameObject.transform.position;
    public int PlayerLayer => gameObject.layer;

    private void Start()
    {
        
    }
}