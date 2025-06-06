using UnityEngine;
using Zenject;

public class PlayerJumpHandler : MonoBehaviour, IPauseHandler
{
    [Header("Jump stats")]
    [SerializeField, Min(0f)] private float _maxJumpTime;
    [SerializeField, Min(0f)] private float _maxJumpHeight;

    [Header("Gravity handling")]
    [SerializeField, Min(0f)] private float _gravityForce = 9.81f;

    private CharacterController _characterController;
    private PlayerMovement _playerMovement;
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
    }

    private void OnDisable()
    {
        _input.OnSpaceClicked -= Jump;
        _input.OnGravityChange -= GravityHandling;
    }

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
}
