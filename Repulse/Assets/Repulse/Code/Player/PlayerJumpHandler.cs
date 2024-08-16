using UnityEngine;

public class PlayerJumpHandler : MonoBehaviour
{
    [Header("Jump stats")]
    [SerializeField, Min(0f)] private float _maxJumpTime;
    [SerializeField, Min(0f)] private float _maxJumpHeight;

    [Header("Gravity handling")]
    [SerializeField, Min(0f)] private float _gravityForce = 9.81f;

    private CharacterController _characterController;
    private PlayerMovement _playerMovement;

    private float startJumpVelocity;
    private Vector3 velocityDirection;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerMovement = GetComponent<PlayerMovement>();

        float maxHeightTime = _maxJumpTime / 2;
        _gravityForce = (2 * _maxJumpHeight) / Mathf.Pow(maxHeightTime, 2);
        startJumpVelocity = (2 * _maxJumpHeight) / maxHeightTime;
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
            _playerMovement.VelocityDirectionY = startJumpVelocity;
    }

    public void GravityHandling()
    {
        if (!_characterController.isGrounded)
            _playerMovement.VelocityDirectionY -= _gravityForce * Time.deltaTime;
        else
            _playerMovement.VelocityDirectionY = -10f;
    }
}
