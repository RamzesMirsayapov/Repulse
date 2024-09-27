using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PointDisplayer : MonoBehaviour
{
    private Image image;

    private Dictionary<TargetPointer, Image> _targetPointers = new Dictionary<TargetPointer, Image>();

    private Transform _playerTransform;
    private Camera _camera;

    private readonly string _playerPointDisplayerName = "PlayerPointDisplayer";

    [Inject]
    private void Construct(PlayerMovement playerMovement, Camera camera)
    {
        _playerTransform = playerMovement.transform.Find(_playerPointDisplayerName);
        _camera = camera;
        Debug.Log(_playerTransform);
        image = Resources.Load<Image>("TargetIndicator");
    }

    public void AddTargetToList(TargetPointer targetPointer)
    {
        Image newImage = Instantiate(image, transform.position, Quaternion.identity, transform);
        _targetPointers.Add(targetPointer, newImage);
    }

    public void RemoveFromList(TargetPointer targetPointer)
    {
        Destroy(_targetPointers[targetPointer].gameObject);
        _targetPointers.Remove(targetPointer);
    }

    private void LateUpdate()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        foreach (var kvp in _targetPointers)
        {
            TargetPointer targetPointer = kvp.Key;
            Image image = kvp.Value;

            Vector3 toEnemy = targetPointer.transform.position - _playerTransform.position;
            Ray ray = new Ray(_playerTransform.position, toEnemy);

            Debug.DrawRay(_playerTransform.position, toEnemy);

            float rayMinDistance = Mathf.Infinity;
            int index = 0;

            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }
                }
            }

            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toEnemy.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            Quaternion rotation = GetIconRotation(index);

            if (toEnemy.magnitude > rayMinDistance)
            {
                image.enabled = true;
            }
            else
            {
                image.enabled = false;
            }

            //pointerIcon.SetIconPosition(position, rotation);
            image.transform.position = position;
            image.transform.rotation = rotation;
        }
    }

    Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex == 0)
        {
            return Quaternion.Euler(0f, 0f, 90f);
        }
        else if (planeIndex == 1)
        {
            return Quaternion.Euler(0f, 0f, -90f);
        }
        else if (planeIndex == 2)
        {
            return Quaternion.Euler(0f, 0f, 180);
        }
        else if (planeIndex == 3)
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        return Quaternion.identity;
    }
}
