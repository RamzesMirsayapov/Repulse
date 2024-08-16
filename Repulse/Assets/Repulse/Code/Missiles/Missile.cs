using UnityEngine;

public abstract class Missile : MonoBehaviour, IReflectable
{
    [SerializeField] protected float _speed = 40f;

    [SerializeField] protected Rigidbody _rigidbody;

    private bool _isReflected = false;

    public bool IsReflected => _isReflected;

    protected void Initialize(float speed)
    {
        _speed = speed;
    }

    protected virtual void Move()
    {
        _rigidbody.velocity = transform.forward * _speed;
    }

    public void ReflectionMove(Vector3 direction)
    {
        if (direction == null)
        {
            Debug.Log("Направление равно нулю");
            return;
        }

        _isReflected = true;

        Vector3 newDirection = direction - transform.position;
        newDirection.Normalize();

        _rigidbody.MoveRotation(Quaternion.LookRotation(newDirection));
    }
}
