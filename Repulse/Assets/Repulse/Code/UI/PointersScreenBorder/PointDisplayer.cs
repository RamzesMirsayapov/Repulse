using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PointDisplayer : MonoBehaviour
{
    private PointerIcon _pointerPrefab;

    private Dictionary<TargetPointer, PointerIcon> _targetPointers = new Dictionary<TargetPointer, PointerIcon>();

    private Transform _playerTransform;
    private Camera _camera;

    private Canvas _canvas;

    private readonly string _playerPointDisplayerName = "PlayerPointDisplayer";

    RectTransform _rectTransform;

    [Inject]
    private void Construct(PlayerMovement playerMovement, Camera camera, PointerIcon pointerIcon)
    {
        _playerTransform = playerMovement.transform.GetChild(0).GetChild(1);

        _camera = camera;

        //Debug.Log("Paretn transform position:" + _camera.transform.parent.position);
        //Debug.Log("Child transform position:" + _camera.transform.position);
        //Debug.Log("Local transform position:" + _camera.transform.localPosition);

        _pointerPrefab = pointerIcon;

        _canvas = GetComponent<Canvas>();

        _rectTransform = GetComponent<RectTransform>();
        //_canvas.worldCamera = _camera;
        Debug.Log(_playerTransform);
    }

    public void AddTargetToList(TargetPointer targetPointer)
    {
        PointerIcon newImage = Instantiate(_pointerPrefab, transform);
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
            PointerIcon image = kvp.Value;

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
            Debug.Log("GetPoint" + worldPosition);
            //float scaleFactor = _canvas.scaleFactor;

            Vector3 position = _camera.WorldToScreenPoint(worldPosition);  // WorldToScreenPoint

            //Vector3 finalPosition = _camera.ScreenToWorldPoint(position);

            //Vector2 finalPosition = new Vector2(position.x / scaleFactor, position.y / scaleFactor);

            //_rectTransform.anchoredPosition = finalPosition;

            Quaternion rotation = GetIconRotation(index);

            if (toEnemy.magnitude > rayMinDistance)
            {
                image.Show();
            }
            else
            {
                image.Hide();
            }

            Debug.Log(toEnemy.magnitude > rayMinDistance);
            image.SetIconPosition(position, rotation);
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
