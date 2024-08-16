using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Stats")]
    [SerializeField, Min(0f)] private float _moveSpeed;

    [Header("CharacterComponent")]
    [SerializeField] private CharacterController _characterController;

    public CharacterController CharacterController => _characterController;

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
