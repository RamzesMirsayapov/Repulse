using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScreenEffectController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private Volume _volume;

    [Header("Effect Settings")]
    [SerializeField] private float _boostEffectIntensity = 0.5f;

    [Header("Default Settings")]
    [SerializeField] private Color _defaultColor = Color.black;
    [SerializeField, Range(0f, 1f)] private float _defaultIntensity = 0.25f;

    private Vignette _vignette;

    private void Awake()
    {
        _volume.profile.TryGet(out _vignette);
    }

    public void SetGlow(Color color)
    {
        if (_vignette != null)
        {
            _vignette.color.value = color;
            _vignette.intensity.value = _boostEffectIntensity;
        }
    }

    public void ResetGlow()
    {
        if (_vignette != null)
        {
            _vignette.color.value = _defaultColor;
            _vignette.intensity.value = _defaultIntensity;
        }
    }
}