using DG.Tweening;
using UnityEngine;
using System;

[Serializable]
public class AnimationPreset
{
    [SerializeField] private bool _isOn = true;
    [SerializeField] private float _duration = 1f;
    [SerializeField] private Ease _ease = DOTween.defaultEaseType;

    public bool IsOn => _isOn;
    public float Duration => _duration;
    public Ease Ease => _ease;
}