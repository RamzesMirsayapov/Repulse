using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class ShakeAnimationPreset : AnimationPreset
{
    [SerializeField] private ShakeRandomnessMode _randomnessMode = ShakeRandomnessMode.Full;
    [SerializeField] private Vector3 _strength;
    [SerializeField] private float _randomness = 90f;
    [SerializeField] private int _vibrato = 10;
    [SerializeField] private bool _snapping;

    public ShakeRandomnessMode RandomnessMode => _randomnessMode;
    public Vector3 Strength => _strength;
    public float Randomness => _randomness;
    public int Vibrato => _vibrato;
    public bool Snapping => _snapping;
}
