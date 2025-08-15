using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Image _arrowImage;
    [SerializeField] private Image _iconImage;

    private Transform _target;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Setup(Transform target, Sprite icon = null)
    {
        _target = target;

        if (icon != null && _iconImage != null)
        {
            _iconImage.sprite = icon;
            _iconImage.gameObject.SetActive(true);
        }
    }

    public void SetPosition(Vector2 screenPosition)
    {
        _rectTransform.position = screenPosition;
    }

    public void SetRotation(float angle)
    {
        float discreteAngle = Mathf.Round(angle / 90) * 90;
        _rectTransform.localEulerAngles = new Vector3(0, 0, discreteAngle);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
