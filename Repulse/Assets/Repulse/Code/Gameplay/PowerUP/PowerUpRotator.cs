using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float _rotationSpeed = 30f;
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;

    private void Update()
    {
        transform.Rotate(_rotationAxis, _rotationSpeed * Time.deltaTime);
    }
}
