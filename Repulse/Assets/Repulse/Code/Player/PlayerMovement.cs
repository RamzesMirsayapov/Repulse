using UnityEngine;
using UnityEngine.Windows;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField, Min(0f)] private float _moveSpeed;

    [Header("CharacterComponent")]
    [SerializeField] private CharacterController _characterController;

    public CharacterController CharacterController => _characterController;

    private IInput _input;

    [Inject]
    private void Construct(IInput input)
    {
        _input = input;

        _input.OnDirectionMove += Move;
    }

    private void OnDisable()
    {
        _input.OnDirectionMove -= Move;
    }

    public float VelocityDirectionY
    {
        get { return _velocityDirectionY; }
        set { _velocityDirectionY = value; }
    }

    private float _velocityDirectionY;

    private Vector3 _velocityDirection;
    private Vector3 _xzDirection;

    public void Move(Vector3 moveDirection)
    {
        _xzDirection = (moveDirection.x * transform.right) + (moveDirection.z * transform.forward);
        _xzDirection = Vector3.ClampMagnitude(_xzDirection, 1f) * _moveSpeed;

        _velocityDirection = new Vector3(_xzDirection.x, VelocityDirectionY, _xzDirection.z);

        _characterController.Move(_velocityDirection * Time.deltaTime);
    }
}
