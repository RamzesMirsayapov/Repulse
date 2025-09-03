using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IndicatorSystem : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private RectTransform _indicatorsCanvas;
    [SerializeField] private GameObject _indicatorPrefab;
    [SerializeField] private float _screenBorderOffset = 50f;

    private Camera _targetCamera;

    private Dictionary<Transform, Indicator> _trackedObjects = new Dictionary<Transform, Indicator>();
    private Queue<Indicator> _indicatorsPool = new Queue<Indicator>();

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _targetCamera = player.Camera;
        _player = player;
    }

    public void RegisterObject(Transform target, Sprite icon = null)
    {
        if (target == null || _trackedObjects.ContainsKey(target)) return;

        var indicator = GetIndicator();
        if (indicator != null)
        {
            indicator.Setup(target, icon);
            _trackedObjects.Add(target, indicator);
        }
    }

    public void UnregisterObject(Transform target)
    {
        if (target == null || !_trackedObjects.ContainsKey(target)) return;

        var indicator = _trackedObjects[target];
        _trackedObjects.Remove(target);

        if (indicator != null && indicator.gameObject != null)
        {
            ReturnIndicatorToPool(indicator);
        }
    }

    private void LateUpdate()
    {
        var toRemove = new List<Transform>();

        foreach (var pair in _trackedObjects)
        {
            if (pair.Key == null || pair.Value == null)
            {
                toRemove.Add(pair.Key);
                continue;
            }

            UpdateIndicatorPosition(pair.Value, pair.Key);
        }

        foreach (var key in toRemove)
        {
            _trackedObjects.Remove(key);
        }
    }

    private void UpdateIndicatorPosition(Indicator indicator, Transform target)
    {
        if (indicator == null || target == null) return;

        var screenPos = _targetCamera.WorldToScreenPoint(target.position);

        if (IsOnScreen(screenPos))
        {
            indicator.Hide();
            return;
        }

        indicator.Show();

        var isBehindCamera = screenPos.z < 0;
        if (isBehindCamera)
        {
            screenPos *= -1;
        }

        var edgePosition = GetClampedScreenPosition(screenPos);
        indicator.SetPosition(edgePosition);

        var viewportPos = _targetCamera.WorldToViewportPoint(target.position);
        if (isBehindCamera)
        {
            viewportPos = new Vector3(1 - viewportPos.x, 1 - viewportPos.y, viewportPos.z);
        }

        var discreteRotation = CalculateDiscreteRotation(viewportPos);
        indicator.SetRotation(discreteRotation);
    }

    private bool IsOnScreen(Vector3 screenPos)
    {
        return screenPos.z > 0 &&
               screenPos.x > 0 && screenPos.x < Screen.width &&
               screenPos.y > 0 && screenPos.y < Screen.height;
    }

    private Vector3 GetClampedScreenPosition(Vector3 screenPos)
    {
        var screenCenter = new Vector3(Screen.width, Screen.height, 0) * 0.5f;
        var direction = (screenPos - screenCenter).normalized;
        var screenRatio = (float)Screen.width / Screen.height;
        var ratioDirection = direction.x / direction.y;

        var isCloserToHorizontal = Mathf.Abs(ratioDirection) > screenRatio;
        Vector3 edgePosition;

        if (isCloserToHorizontal)
        {
            var x = direction.x > 0 ? Screen.width - _screenBorderOffset : _screenBorderOffset;
            var y = screenCenter.y + (x - screenCenter.x) * direction.y / direction.x;
            edgePosition = new Vector3(x, y, 0);
        }
        else
        {
            var y = direction.y > 0 ? Screen.height - _screenBorderOffset : _screenBorderOffset;
            var x = screenCenter.x + (y - screenCenter.y) * direction.x / direction.y;
            edgePosition = new Vector3(x, y, 0);
        }

        edgePosition.x = Mathf.Clamp(edgePosition.x, _screenBorderOffset, Screen.width - _screenBorderOffset);
        edgePosition.y = Mathf.Clamp(edgePosition.y, _screenBorderOffset, Screen.height - _screenBorderOffset);

        return edgePosition;
    }

    private float CalculateDiscreteRotation(Vector3 viewportPos)
    {
        var leftDist = viewportPos.x;
        var rightDist = 1 - viewportPos.x;
        var bottomDist = viewportPos.y;
        var topDist = 1 - viewportPos.y;

        var minDist = Mathf.Min(leftDist, rightDist, bottomDist, topDist);

        if (minDist == leftDist) return 90f;
        if (minDist == rightDist) return -90f;
        if (minDist == bottomDist) return 180f;

        return 0f;
    }

    private Indicator GetIndicator()
    {
        while (_indicatorsPool.Count > 0 && _indicatorsPool.Peek() == null)
        {
            _indicatorsPool.Dequeue();
        }

        if (_indicatorsPool.Count > 0)
        {
            return _indicatorsPool.Dequeue();
        }

        var indicatorObj = Instantiate(_indicatorPrefab, _indicatorsCanvas);
        return indicatorObj.GetComponent<Indicator>();
    }

    private void ReturnIndicatorToPool(Indicator indicator)
    {
        if (indicator == null || indicator.gameObject == null) return;

        indicator.Hide();
        _indicatorsPool.Enqueue(indicator);
    }
}