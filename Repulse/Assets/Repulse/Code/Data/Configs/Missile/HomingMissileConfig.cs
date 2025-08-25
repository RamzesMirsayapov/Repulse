using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Missile/HomingMissilePreset", fileName = "HomingMissilePreset")]
public class HomingMissileConfig : ScriptableObject
{
    [Header("Rotate speed")]
    [SerializeField] private float _rotateSpeed = 200f;

    [Header("Predication")]
    [SerializeField] private float _maxDistancePredict = 100f;
    [SerializeField] private float _minDistancePredict = 20f;
    [SerializeField] private float _maxTimePrediction = 0.1f;

    [Header("Deviation")]
    [SerializeField] private float _deviationAmount = 25f;
    [SerializeField] private float _deviationSpeed = 2f;

    [Header("Explosion Settings")]
    [SerializeField] private MissileExplosionConfig _missileExplosionConfig;

    public float RotateSpeed => _rotateSpeed;

    public float MaxDistancePredict => _maxDistancePredict;
    public float MinDistancePredict => _minDistancePredict;
    public float MaxTimePredication => _maxTimePrediction;

    public float DeviationAmount => _deviationAmount;
    public float DeviationSpeed => _deviationSpeed;

    public MissileExplosionConfig MissileExplosionConfig => _missileExplosionConfig;
}
