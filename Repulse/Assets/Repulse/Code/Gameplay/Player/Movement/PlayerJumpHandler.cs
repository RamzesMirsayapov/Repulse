using UnityEngine;
using Zenject;

public class PlayerJumpHandler : MonoBehaviour, IPauseHandler
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private PlayerMovement _playerMovement;

    [Header("Jump stats")]
    [SerializeField, Min(0f)] private float _maxJumpTime;
    [SerializeField, Min(0f)] private float _maxJumpHeight;

    [Header("Gravity handling")]
    [SerializeField, Min(0f)] private float _gravityForce = 9.81f;

    private PauseManager _pauseManager;

    private IInput _input;
       
    private Vector3 velocityDirection;
    private float startJumpVelocity;

    private bool _isPaused;

    [Inject]
    private void Construct(IInput input, PauseManager pauseManager)
    {
        _input = input;
        _pauseManager = pauseManager;

        _input.OnSpaceClicked += Jump;
        _input.OnGravityChange += GravityHandling;

        _pauseManager.Register(this);

        CalculateGravityForce();
    }

    private void OnDisable()
    {
        _input.OnSpaceClicked -= Jump;
        _input.OnGravityChange -= GravityHandling;
    }

    public void Jump()
    {
        if(_isPaused)
            return;

        if (_characterController.isGrounded)
            _playerMovement.VelocityDirectionY = startJumpVelocity;
    }

    public void GravityHandling()
    {
        if (_isPaused)
            return;

        if (!_characterController.isGrounded)
            _playerMovement.VelocityDirectionY -= _gravityForce * Time.deltaTime;
        else
            _playerMovement.VelocityDirectionY = -10f;
    }

    public void SetPaused(bool isPaused)
    {
        _isPaused = isPaused;
    }

    private void CalculateGravityForce()
    {
        float maxHeightTime = _maxJumpTime / 2;

        _gravityForce = (2 * _maxJumpHeight) / Mathf.Pow(maxHeightTime, 2);
        startJumpVelocity = (2 * _maxJumpHeight) / maxHeightTime;
    }
}