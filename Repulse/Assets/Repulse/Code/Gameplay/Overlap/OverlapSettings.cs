using System;
using UnityEngine;

[Serializable]
public class OverlapSettings
{
    [Header("Common")]
    [SerializeField] private LayerMask _searchMask;
    [SerializeField] private Transform _overlapPoint;

    [Header("Overlap Area")]
    [SerializeField] private OverlapType _overlapType;
    [SerializeField] private Vector3 _boxSize = Vector3.one;
    [SerializeField, Min(0f)] private float _sphereRadius = 0.5f;

    [Header("Offset")]
    [SerializeField] private Vector3 _positionOffset;

    [Header("Obstacles")]
    [SerializeField] private bool _considerObstacles;
    [SerializeField] private LayerMask _obstacleMask;

    [Header("Gizmos")]
    [SerializeField] private bool _drawGizmos = true;
    [SerializeField] private Color _gizmosColor = Color.cyan;

    [Header("Particle")]
    [SerializeField] private ParticleSystem _effectPrefab;
    [SerializeField] private float _effectPrefabLifeTime;

    [NonSerialized] public float Size;

    public readonly Collider[] OverlapResults = new Collider[32];

    public LayerMask SearchMask => _searchMask;
    public Transform OverlapPoint => _overlapPoint;
    public OverlapType OverlapType => _overlapType;
    public Vector3 BoxSize => _boxSize;
    public float SphereRadius => _sphereRadius;
    public Vector3 Offset => _positionOffset;
    public bool ConsiderObstacles => _considerObstacles;
    public LayerMask ObstacleMask => _obstacleMask;
    public ParticleSystem EffectPrefab => _effectPrefab;
    public float EffectPrefabLifeTime => _effectPrefabLifeTime;

    public void TryDrawGizmos()
    {
        if (_drawGizmos == false)
            return;

        if (_overlapPoint == null)
            return;

        Gizmos.matrix = _overlapPoint.localToWorldMatrix;
        Gizmos.color = _gizmosColor;

        switch(_overlapType)
        {
            case OverlapType.box: Gizmos.DrawCube(_positionOffset, _boxSize); break;
            case OverlapType.sphere: Gizmos.DrawSphere(_positionOffset, _sphereRadius); break;

            default: throw new ArgumentOutOfRangeException(nameof(_overlapType));
        }    
    }
}
