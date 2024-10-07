using UnityEngine;
using Zenject;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerRotation _playerRotation;
    [SerializeField] private PlayerJumpHandler _jumpHandler;

    private float horizontal;
    private float vertical;

    private float inputX;
    private float inputY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        inputX = Input.GetAxis("Mouse X");
        inputY = Input.GetAxis("Mouse Y");

        _jumpHandler.GravityHandling();

        if (Input.GetKeyDown(KeyCode.Space)) //должно идти после вверхнего метода хз почему
            _jumpHandler.Jump();

        _playerRotation.Rotate(inputX, inputY);
        _playerMovement.Move(new Vector3(horizontal, 0f, vertical));
    }
}
